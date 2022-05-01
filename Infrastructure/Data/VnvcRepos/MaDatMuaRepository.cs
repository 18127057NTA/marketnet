using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.VNVCModels;
using MongoDB.Driver;

namespace Infrastructure.Data.VnvcRepos
{
    public class MaDatMuaRepository
    {
        private readonly IMongoCollection<MaDatMua> _mdmCollection;
        private readonly IMongoClient _mongoClient;

        public MaDatMuaRepository(IMongoClient mongoClient){
            _mongoClient = mongoClient;
            _mdmCollection = mongoClient.GetDatabase("vnvc-demo").GetCollection<MaDatMua>("ma-dat-mua");
        }

        public async Task<MaDatMua> CreateMDMAsync(MaDatMua mdm)
        {
            await _mdmCollection.InsertOneAsync(mdm);

            return await _mdmCollection.Aggregate()
                .Match(Builders<MaDatMua>.Filter.Eq(x => x.SdtKH, mdm.SdtKH)).FirstOrDefaultAsync();
        }

        public async Task<MaDatMua> GetMDMByBasketIdAsync(string basketid){
            return await _mdmCollection.Aggregate()
                .Match(Builders<MaDatMua>.Filter.Eq(x => x.MaGioHang, basketid)).FirstOrDefaultAsync();
        }
    }
}