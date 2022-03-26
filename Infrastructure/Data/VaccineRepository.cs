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
        public async Task<long> GetVaccinesCountAsync(){
            return await _vaccinesCollection.CountDocumentsAsync(Builders<Vaccine>.Filter.Empty);
        }

        public async Task<Vaccine> GetVaccineAsync(string vaccineId){
            try{
                return await _vaccinesCollection.Aggregate()
                    .Match(Builders<Vaccine>.Filter.Eq(x => x.Id, vaccineId)).FirstOrDefaultAsync();
            }   
            catch(Exception ex){
                throw;
            }
        }
    }
}