
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Core.Entities.VNVCModels
{
    public class NgTiem
    {
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("hovaten")]
        [JsonProperty("hovaten")]
        public string HoVaTen { get; set; }
        [BsonElement("ngaysinh")]
        [JsonProperty("ngaysinh")]
        public string NgaySinh { get; set; }
        [BsonElement("gioitinh")]
        [JsonProperty("gioitinh")]
        public string GioiTinh { get; set; }
        [BsonElement("sodienthoai")]
        [JsonProperty("sodienthoai")]
        public string SoDienThoai { get; set; }
        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        [BsonElement("diachi")]
        [JsonProperty("diachi")]
        public string DiaChi { get; set; }
    }
}