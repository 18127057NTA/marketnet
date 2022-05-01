using API.Dtos;
using API.Dtos.VnvcDtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.VNVCModels;
using Core.Interfaces;
using Core.Interfaces.VnvcInterfaces;
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
        private readonly IVnvcRDbRepository _vnvcRDbRepository;
        //Giỏ hàng
        private readonly IBasketRepository _basketRepository;
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            //Người tiêm repo
            NgTiemRepository ngTiemRepo,
            //Vnvc sql repo
            IVnvcRDbRepository vnvcRDbRepo,
            //Mã đặt mua repo
            MaDatMuaRepository mdmRepo,
            //Giỏ hàng
            IBasketRepository basketRepo
            
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _ngTiemRepository = ngTiemRepo;
            _vnvcRDbRepository = vnvcRDbRepo;
            _mdmRepository = mdmRepo;
            _basketRepository = basketRepo;
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

        /*public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }*/
        [HttpPut("address")]
        public async Task<ActionResult<ThongTinNguoiMuaDto>> UpdateUserAddress(ThongTinNguoiMuaDto ttnguoimua)
        {
            var khachHang = new KhachHang
            {
                HoTen = ttnguoimua.HoTen,
                Cccd = ttnguoimua.Cccd,
                Sdt = ttnguoimua.Sdt,
                Email = ttnguoimua.Email,
                DiaChi = ttnguoimua.DiaChi
            };
            //Tạo người mua mới nếu chưa có
            var ngMua = await _vnvcRDbRepository.CreateNgMuaAsync(khachHang);
            //Tạo hóa đơn mới - thiếu ngày tiêm - chưa tính tổng tiền(đợi phương thức thanh toán)
            var hoaDonMs = new DonHang {
                ChiNhanhId = ttnguoimua.ChiNhanhTiem,
                KhachHangId = ngMua.Id,
                NgayMua = DateTime.Now,
                TinhTrang = "Chua thanh toan"
            };
            var donHangMs = _vnvcRDbRepository.CreateDonHangAsync(hoaDonMs).Result;
            //Cập nhật danh sách người tiêm
            await _ngTiemRepository.UpdateDsNgTiemWithMaHoaDonAsync(ttnguoimua.MaGioHang, donHangMs.Id);
            //Lấy thông tin giỏ hàng hiện tại
            var gioHang = _basketRepository.GetBasketAsync(ttnguoimua.MaGioHang).Result;
            //Bắt đầu tính tổng tiền
            var tongTien = 0;
            //Với mỗi mặt hàng trong giỏ hàng
            foreach (var item in gioHang.Items){
                //Tạo một chi tiết đơn hàng mới
                var ctdh = new ChiTietDonHang {
                    DonGia = item.Gia,
                    DonHangId = donHangMs.Id,
                    MaSanPham = item.Id,
                    SoLuong = item.SoLuongGoi,
                   TenSanPham = item.Ten
                };
                tongTien = tongTien + ctdh.DonGia * ctdh.SoLuong;
                //Tạo đơn hàng mới
                await _vnvcRDbRepository.CreateCTDHAsync(ctdh);
            }
            //Thêm tổng tiền vào hóa đơn
            //await _vnvcRDbRepository.UpdateDonHangByTongTien(donHangMs.Id, tongTien);

            //Cập nhật tổng tiền cho giỏ hàng
            gioHang.Total = tongTien;
            //Tạo mã đặt mua mới
            var maDatMua = new MaDatMua
            {
                MaKH = ngMua.Id,
                MaGioHang = ttnguoimua.MaGioHang,
                SdtKH = ngMua.Sdt,
                MaDonHang = donHangMs.Id
            };
            await _mdmRepository.CreateMDMAsync(maDatMua);
            //Trả về thông tin người mua kèm mã giỏ hàng*/
            
            //Cập nhật mã giỏ hàng
            gioHang.TTChuyenKhoan = maDatMua.SdtKH + "_" + maDatMua.Id;
            await _basketRepository.UpdateBasketAsync(gioHang);

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