using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class DON_HANG
    {
        public int DH_ID {get; set;}
        public CHI_NHANH CHI_NHANH {get; set;}
        //id của chi nhánh tiêm
        [Column(TypeName = "nvarchar")]
        public string DH_IDCN {get; set;} // Vấn đề
        public KHACH_HANG KHACH_HANG {get; set;}
        public int DH_IDKH {get; set;}
        public DateTime DH_NGAY {get; set;}
        public DateTime DH_NGAYTIEM {get; set;}
        public int DH_TONGTIEN {get; set;}
        [Column(TypeName = "nvarchar")]
        public string DH_TTRANG {get; set;}

    }
}