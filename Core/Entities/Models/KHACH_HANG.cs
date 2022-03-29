using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Models
{
    public class KHACH_HANG
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        public string KH_CCCD { get; set;}
        
        /*[Column(TypeName = "nvarchar")]
        public string KH_ID { get; set; }*/
        [Column(TypeName = "nvarchar")]
        public string KH_HOTEN { get; set; }
        [Column(TypeName = "nvarchar")]
        public string KH_EMAIL { get; set; }
        [Column(TypeName = "nvarchar")]
        public string KH_SDT { get; set; }
        [Column(TypeName = "nvarchar")]
        public string KH_DIACHI { get; set; }
    }
}