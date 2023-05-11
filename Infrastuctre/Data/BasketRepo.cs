using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastuctre.Data
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _db;

        public  BasketRepo(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _db.KeyDeleteAsync(basketid);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketid)
        {
            var item = await _db.StringGetAsync(basketid);
            return item.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(item);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _db.StringSetAsync(
                basket.Id, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
