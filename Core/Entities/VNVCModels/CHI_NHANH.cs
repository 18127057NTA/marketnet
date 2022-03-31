using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class CHI_NHANH
    {

        [Key]
        [Column(TypeName = "nvarchar")]
        public string CN_ID { get; set; }
        //Khóa ngoại id tỉnh thành
        public TINH_THANH TINH_THANH { get; set; }
        [Column(TypeName = "nvarchar")]
        //public string TT_ID {get; set;}
        public string CN_IDTINH { get; set; }
        //Tên chi chánh
        [Column(TypeName = "nvarchar")]
        public string CN_TENCN { get; set; }
        //Địa chỉ
        [Column(TypeName = "nvarchar")]
        public string CN_DIACHI { get; set; }
    }
}