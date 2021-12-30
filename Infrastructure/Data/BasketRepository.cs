using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<BuyerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<BuyerBasket>(data);
        }

        public async Task<BuyerBasket> UpdateBasketAsync(BuyerBasket basket)
        {
            var created = await _database.StringSetAsync(
                basket.Id, 
                JsonSerializer.Serialize(basket), 
                TimeSpan.FromDays(30)
            );

            if(!created) return null;

            return await GetBasketAsync(basket.Id);

        }
    }
}