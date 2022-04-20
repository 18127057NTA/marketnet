using API.Dtos;
using API.Dtos.VnvcDtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.VNVCModels;
using Core.Interfaces;
using Infrastructure.Data.VnvcRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        //Người tiêm và danh sách tiểm
        private readonly NgTiemRepository _ngTiemRepository;
        //Mã đặt mua
        private readonly MaDatMuaRepository _mdmRepository;
        //Repo khách hàng sql
        //Repo tỉnh
        //Repo chi nhánh
        //Tất cả 3 cái trên vô 1 cái repo
        private readonly VnvcRDbRepository _vnvcRDbRepository;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            //Người tiêm repo
            NgTiemRepository ngTiemRepo,
            //Vnvc sql repo
            VnvcRDbRepository vnvcRDbRepo,
            //Mã đặt mua repo
            MaDatMuaRepository mdmRepo
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _ngTiemRepository = ngTiemRepo;
            _vnvcRDbRepository = vnvcRDbRepo;
            _mdmRepository = mdmRepo;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            //Tốt hơn
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //Trước đó
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);// Có thể chỉ cần User thôi.

            return new UserDto
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        //Kiểm tra xem có tồn tại document ds-nguoi-tiem có mã giỏ hàng hay không?
        public async Task<ActionResult<bool>> CheckDsNgTiemExistsAsync([FromQuery] string magiohang)
        {
            return await _ngTiemRepository.GetDsNgTiemAsync(magiohang) != null;
        }
        //Kiểm tra xem có tồn tại người tiêm với số điện thoại mới hay không ?
        public async Task<ActionResult<bool>> CheckNgTiemExistsAsync(string sodienthoai)
        {
            return await _ngTiemRepository.GetNgTiemAsync(sodienthoai) != null;
        }


        //[Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);

            //return user.Address;
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        //Cập nhật / Nhập thông tin người đặt mua vaccine
        //[Authorize]
        [HttpPut("address")]
        /*public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }*/
        public async Task<ActionResult<ThongTinNguoiMuaDto>> UpdateUserAddress(ThongTinNguoiMuaDto ttnguoimua)
        {
            var khMoi = new KHACH_HANG
            {
                KH_HOTEN = ttnguoimua.HoTen,
                KH_CCCD = ttnguoimua.Cccd,
                KH_SDT = ttnguoimua.Sdt,
                KH_EMAIL = ttnguoimua.Email,
                KH_DIACHI = ttnguoimua.DiaChi
            };
            //Kiểm tra người mua đã tồn tại hay chưa?
            var user = await _vnvcRDbRepository.GetNgMuaTheoCccdAsync(khMoi.KH_CCCD);
            //Nếu user đã tồn tại
            if (user != null)
            {
                //Lấy mã khách hàng
                khMoi.Id = user.Id;
                //Cập nhật số điện thoại người mua nếu có thay đổi
                if (user.KH_SDT != khMoi.KH_SDT)
                {
                    await _vnvcRDbRepository.UpdateNgMuaBySdt(khMoi.KH_CCCD, khMoi.KH_SDT);
                }
                var mDMMoi = new MaDatMua
                {
                    MaKH = user.Id,
                    MaGioHang = ttnguoimua.MaGioHang,
                    SdtKH = ttnguoimua.Sdt
                };
                await _mdmRepository.CreateMDMAsync(mDMMoi);
            }
            else
            {
                //Thêm khách hàng mới
                //Id của khách hàng mới đang bằng null -> nếu insert bị lỗi -> select count và + 1 sau đó gán vào id
                var khTaoMoi = await _vnvcRDbRepository.CreateNgMuaAsync(khMoi);
                //Tạo document mới trong ma-dat-mua
                var mDMMoi = new MaDatMua
                {
                    MaKH = khTaoMoi.Id,
                    MaGioHang = ttnguoimua.MaGioHang,
                    SdtKH = ttnguoimua.Sdt
                };
                await _mdmRepository.CreateMDMAsync(mDMMoi);
            }
            //Trả về thông tin người mua kèm mã giỏ hàng
            return ttnguoimua;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
        //Đăng ký người tiêm
        [HttpPost("register")]
        //public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) //registerDto phải map vs form group
        public async Task<ActionResult<NgTiemToReturnDto>> Register(NgTiemDto ngtiemDto)
        {
            //C2
            //Check xem ds có tồn tại không ?-> tạo mới
            if (CheckDsNgTiemExistsAsync(ngtiemDto.MaGioHang).Result.Value == false) //Nếu ko có
            {
                var DsNgTiemMoi = new DsNgTiem
                {
                    MaHoaDon = 0,
                    MaGioHang = ngtiemDto.MaGioHang,
                    Dsngtiem = new List<string>()
                };
                await _ngTiemRepository.CreateDsNgTiemAsync(DsNgTiemMoi);
            }
            //Check người tiêm có tồn tại = số điện thoại trên collection ?- tạo mới
            if (CheckNgTiemExistsAsync(ngtiemDto.SoDienThoai).Result.Value == false) //Nếu ko có
            {
                //Tạo người tiêm mới
                var ngTiemMs = new NgTiem
                {
                    HoVaTen = ngtiemDto.HoVaTen,
                    NgaySinh = ngtiemDto.NgaySinh,
                    GioiTinh = ngtiemDto.GioiTinh,
                    SoDienThoai = ngtiemDto.SoDienThoai,
                    Email = ngtiemDto.Email,
                    DiaChi = ngtiemDto.DiaChi
                };
                await _ngTiemRepository.CreateNgTiemAsync(ngTiemMs);

                //Test
                /*return new NgTiemToReturnDto
                {
                    Id = "1",
                    HoVaTen = ngtiemDto.HoVaTen,
                    NgaySinh = ngtiemDto.NgaySinh,
                    GioiTinh = ngtiemDto.GioiTinh,
                    SoDienThoai = ngtiemDto.SoDienThoai,
                    Email = ngtiemDto.Email,
                    DiaChi = ngtiemDto.DiaChi
                };*/
            }
            //Check người tiêm có tồn tại trên ds tiêm (string) ? -> thêm
            var dsNgTiemHienTai = _ngTiemRepository.GetDsNgTiemAsync(ngtiemDto.MaGioHang).Result;
            var ngTiemHienTai = _ngTiemRepository.GetNgTiemAsync(ngtiemDto.SoDienThoai).Result;

            if (dsNgTiemHienTai.Dsngtiem.Contains(ngTiemHienTai.Id) == false)
            {
                //Cập nhật lại danh sách -> tạo hàm update trong repo
                await _ngTiemRepository.UpdateDsNgTiemWithNgTiemIdAsync(dsNgTiemHienTai.MaGioHang, ngTiemHienTai.Id);
                /*return new NgTiemToReturnDto
                {
                    Id = "3",
                    HoVaTen = ngtiemDto.HoVaTen,
                    NgaySinh = ngtiemDto.NgaySinh,
                    GioiTinh = ngtiemDto.GioiTinh,
                    SoDienThoai = ngtiemDto.SoDienThoai,
                    Email = ngtiemDto.Email,
                    DiaChi = ngtiemDto.DiaChi
                };*/
            }
            //Trả về NgTiemDto có id
            return new NgTiemToReturnDto
            {
                Id = ngTiemHienTai.Id,
                HoVaTen = ngTiemHienTai.HoVaTen,
                NgaySinh = ngTiemHienTai.NgaySinh,
                GioiTinh = ngTiemHienTai.GioiTinh,
                SoDienThoai = ngTiemHienTai.SoDienThoai,
                Email = ngTiemHienTai.Email,
                DiaChi = ngTiemHienTai.DiaChi
            };

            //Test
            /*return new NgTiemToReturnDto
            {
                Id = ngTiemHienTai.Id,
                HoVaTen = ngtiemDto.HoVaTen,
                NgaySinh = ngtiemDto.NgaySinh,
                GioiTinh = ngtiemDto.GioiTinh,
                SoDienThoai = ngtiemDto.SoDienThoai,
                Email = ngtiemDto.Email,
                DiaChi = ngtiemDto.DiaChi
            };*/

            /*if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Email đã tồn tại trong hệ thống!" } // "Email address is in use"
                });
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                Phone = user.PhoneNumber
            };*/
        }

    }
}