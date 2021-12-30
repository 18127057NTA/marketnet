using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<BuyerBasket> GetBasketAsync(string basketId);

        Task<BuyerBasket> UpdateBasketAsync(BuyerBasket basket);

        Task<bool> DeleteBasketAsync(string basketId);
    }
}