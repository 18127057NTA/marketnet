using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Core.Entities.VNVCModels
{
    public class DsNgTiem
    {
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}
        [BsonElement("mahoadon")]
        [JsonProperty("mahoadon")]
        public int MaHoaDon {get; set;}
        [BsonElement("magiohang")]
        [JsonProperty("magiohang")]
        public string MaGioHang {get; set;}

        [BsonElement("danhsach")]
        [JsonProperty("danhsach")]
        public List<string> Dsngtiem {get; set;}
    }
}