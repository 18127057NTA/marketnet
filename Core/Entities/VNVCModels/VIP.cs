using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Entities.VNVCModels
{
    public class VIP
    {
        //Id
        [Key]
        public int VIP_ID {get; set;}
        //Class khách hàng
        public KHACH_HANG KHACH_HANG { get; set; }
        //Khóa ngoại Id khách hàng
        public int VIP_IDKH { get; set; }
        //Ngày bắt đầu
        [Column(TypeName = "nvarchar")]
        public DateTime VIP_NGAYBD { get; set; }
        //Ngày kết thúc
        [Column(TypeName = "datetime")]
        public DateTime VIP_NGAYKT { get; set; }
    }
}