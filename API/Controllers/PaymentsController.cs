using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.VnvcInterfaces;
using Infrastructure.Data.VnvcRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Order = Core.Entities.OrderAggregate.Order;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private const string WhSecret = "whsec_8gpauLJSMw0iuPAiLRPZkD84w6ph6lnd";//?
        private readonly ILogger<PaymentsController> _logger;
        //basket repo
        private readonly IBasketRepository _basketRepository;
        //vnvc relational db
        private readonly IVnvcRDbRepository _vnvcRepository;
        //Mã đặt mua repo
        private readonly MaDatMuaRepository _mdmRepository;
        public PaymentsController(
            IPaymentService paymentService,
            ILogger<PaymentsController> logger, 
            IBasketRepository basketRepo, 
            IVnvcRDbRepository vnvcRepo,
            MaDatMuaRepository mdmRepo
        )
        {
            _logger = logger;
            _paymentService = paymentService;
            //basket repo
            _basketRepository = basketRepo;
            //vnvc repo
            _vnvcRepository = vnvcRepo;
            //Mã đặt mua repo
            _mdmRepository = mdmRepo;

        }

        //[Authorize]
        //Hiển thị thông tin thanh toán
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BuyerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
             /*var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));*/

            //Lấy giỏ hàng
            //Có thể cần .Result
            var basket = await _basketRepository.GetBasketAsync(basketId);
            //Nếu có mã vip Hợp lệ 
            if (basket.PaymentTypeId == 3 && basket.VipMemberId != null){
                basket.Total = basket.Total * 8 / 10;
                //Cập nhật giỏ hàng
                await _basketRepository.UpdateBasketAsync(basket);
            }
            //Lấy mã đặt mua
            //Có thể cần .Result
            var mdm = await _mdmRepository.GetMDMByBasketIdAsync(basket.Id);
            //Cập nhật hóa đơn
            await _vnvcRepository.UpdateDonHangByTongTien(mdm.MaDonHang, basket.Total);
            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded");
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed: ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment failed: ", order.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}