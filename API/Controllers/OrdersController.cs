using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Entities.VNVCModels;
using Core.Interfaces;
using Infrastructure.Data.VnvcRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        //Mã đặt mua
        private readonly MaDatMuaRepository _mdmRepository;
        public OrdersController(IOrderService orderService, IMapper mapper, MaDatMuaRepository mdmRepo)
        {
            _mapper = mapper;
            _orderService = orderService;
            //Danh sách mã đặt mua
            _mdmRepository = mdmRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var phoneNumber = HttpContext.User.RetrievePhoneNumberFromPrincipal(); //?

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(
                email, 
                phoneNumber,
                orderDto.DeliveryMethodId, 
                orderDto.BasketId, 
                address
            );

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }

        /*[HttpGet("ttChuyenKhoan")]
        public async Task<ActionResult<ThongTinCK>> GetTTChuyenKhoan([FromQuery] string mgh){
            var mdm = await _mdmRepository.GetMDMByBasketIdAsync(mgh);

            var thongTinCK = new ThongTinCK {
                ChuoiKiTu = (mdm.SdtKH + "_" + mdm.Id).ToString()
            };
            return thongTinCK;
        }*/
    }
}
