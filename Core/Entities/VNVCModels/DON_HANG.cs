using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class DON_HANG
    {
        //id
        [Key]
        public int DH_ID {get; set;}
        //Khóa ngoại chi nhánh
        public CHI_NHANH CHI_NHANH {get; set;}
        //id của chi nhánh tiêm
        [Column(TypeName = "nvarchar")]
        public string DH_IDCN {get; set;}
        //Khóa ngoại khách hàng
        public KHACH_HANG KHACH_HANG {get; set;}
        public int DH_IDKH {get; set;}
        //Ngày tạo hóa đơn
        public DateTime DH_NGAY {get; set;}
        //Ngày tiến hành tiêm vax
        public DateTime DH_NGAYTIEM {get; set;}
        //Tổng số tiền
        public int DH_TONGTIEN {get; set;}
        //Tình trạng đơn hàng
        [Column(TypeName = "nvarchar")]
        public string DH_TTRANG {get; set;}
    }
}