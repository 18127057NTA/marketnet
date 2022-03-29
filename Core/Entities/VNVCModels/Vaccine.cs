using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class Vaccine
    {

        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ten")]
        [JsonProperty("ten")]
        public string Ten { get; set; }

        [BsonElement("gia")]
        [JsonProperty("gia")]
        public int Gia { get; set; }

        [BsonElement("motathongtin")]
        [JsonProperty("motathongtin")]
        public string MoTaThongTin { get; set; }

        [BsonElement("phongbenh")]
        [JsonProperty("phongbenh")]
        public string PhongBenh { get; set; }

        [BsonElement("tongsolieu")]
        [JsonProperty("tongsolieu")]

        public int TongSoLieu { get; set; }

        [BsonElement("hinhanh")]
        [JsonProperty("hinhanh")]
        public string HinhAnh { get; set; }

    }
}