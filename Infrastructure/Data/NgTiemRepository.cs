using Core.Entities;
using Core.Entities.VNVCModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class NgTiemRepository
    {
        private readonly IMongoCollection<DsNgTiem> _dsNgTiemCollection;
        private readonly IMongoCollection<NgTiem> _ngTiemCollection;
        private readonly IMongoClient _mongoClient;

        public NgTiemRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            _ngTiemCollection = mongoClient.GetDatabase("vnvc-demo").GetCollection<NgTiem>("nguoi-tiem");
            _dsNgTiemCollection = mongoClient.GetDatabase("vnvc-demo").GetCollection<DsNgTiem>("ds-nguoi-tiem");

        }
        public async Task<DsNgTiem> GetDsNgTiemAsync(string mgh)
        {
            return await _dsNgTiemCollection.Aggregate()
                .Match(Builders<DsNgTiem>.Filter.Eq(x => x.MaGioHang, mgh)).FirstOrDefaultAsync();
        }
        public async Task<NgTiem> GetNgTiemAsync(string sdt)
        {
            return await _ngTiemCollection.Aggregate()
                .Match(Builders<NgTiem>.Filter.Eq(x => x.SoDienThoai, sdt)).FirstOrDefaultAsync();
        }
        
        public async Task<DsNgTiem> CreateDsNgTiemAsync(DsNgTiem ds)
        {
            await _dsNgTiemCollection.InsertOneAsync(ds);
            return await _dsNgTiemCollection.Aggregate()
                .Match(Builders<DsNgTiem>.Filter.Eq(x => x.MaGioHang, ds.MaGioHang)).FirstOrDefaultAsync();
        }
        public async Task<NgTiem> CreateNgTiemAsync(NgTiem ngt)
        {
            await _ngTiemCollection.InsertOneAsync(ngt);

            return await _ngTiemCollection.Aggregate()
                .Match(Builders<NgTiem>.Filter.Eq(x => x.SoDienThoai, ngt.SoDienThoai)).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateDsNgTiemWithNgTiemIdAsync(string mgh2, string idNgTiemMs)
        {
            var filter = Builders<DsNgTiem>.Filter.Eq(ds => ds.MaGioHang, mgh2);
            var update = Builders<DsNgTiem>.Update.Push(ds => ds.Dsngtiem, idNgTiemMs);
            var result = await _dsNgTiemCollection.UpdateOneAsync(filter, update);

            return result.IsAcknowledged && result.ModifiedCount > 0;

        }
    }
}