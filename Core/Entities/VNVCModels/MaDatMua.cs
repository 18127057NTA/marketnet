using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Core.Entities.VNVCModels
{
    public class MaDatMua
    {
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("makh")]
        [JsonProperty("makh")]
        public int MaKH { get; set; }

        [BsonElement("sdtkh")]
        [JsonProperty("sdtkh")]
        public string SdtKH { get; set; }
        [BsonElement("magiohang")]
        [JsonProperty("magiohang")]
        public string MaGioHang { get; set; }
    }
}