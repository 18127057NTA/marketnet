using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class VaccineRepository
    {
        private readonly IMongoCollection<Vaccine> _vaccinesCollection;
        private readonly IMongoClient _mongoClient;

        public VaccineRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            _vaccinesCollection = mongoClient.GetDatabase("vnvc-demo").GetCollection<Vaccine>("vaccine-info");
        }
        public async Task<IReadOnlyList<Vaccine>> GetVaccinesAsync()
        {

            var vaccines = await _vaccinesCollection.Find(Builders<Vaccine>.Filter.Empty).ToListAsync();
            return vaccines; // Vướng async chưa cần sửa 
        }
    }
}