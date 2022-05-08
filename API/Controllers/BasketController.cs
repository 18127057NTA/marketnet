using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BuyerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new BuyerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<BuyerBasket>> UpdateBasket(CustomerBasketDto basket) // trước đó (BuyerBasket basket)
        {
            var buyerBasket = _mapper.Map<CustomerBasketDto, BuyerBasket>(basket);
            //var tempBasket = new BuyerBasket();
            var tempBasket = await _basketRepository.GetBasketAsync(basket.Id);
            
            if(tempBasket != null){

                var newBasket = new BuyerBasket ();

                newBasket.Id = tempBasket.Id;
                newBasket.Items = buyerBasket.Items;
                newBasket.Total = tempBasket.Total;
                newBasket.TTChuyenKhoan = tempBasket.TTChuyenKhoan;
                newBasket.PaymentTypeId = basket.PaymentTypeId;
                newBasket.VipMemberId = tempBasket.VipMemberId;

                var updatedBasket2 = await _basketRepository.UpdateBasketAsync(newBasket);
                return Ok(updatedBasket2);
            }
            
            //var buyerBasket = _mapper.Map<CustomerBasketDto, BuyerBasket>(basket);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(buyerBasket);
    
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}