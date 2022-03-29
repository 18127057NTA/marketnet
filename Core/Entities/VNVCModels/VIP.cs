using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Entities.VNVCModels
{
    public class VIP
    {
        public KHACH_HANG KHACH_HANG { get; set; }
        public int KH_ID { get; set; }
        [Column(TypeName = "nvarchar")]
        public string VIP_IDKH { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VIP_NGAYBD { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VIP_NGAYKT { get; set; }
    }
}