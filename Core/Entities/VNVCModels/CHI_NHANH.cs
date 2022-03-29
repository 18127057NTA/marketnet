using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class CHI_NHANH
    {
        public TINH_THANH TINH_THANH {get; set;}
        [Column(TypeName = "nvarchar")]
        public string TT_ID {get; set;}
        //public string TT_ID {get; set;}
        [Column(TypeName = "nvarchar")]
        public string TT_TENTT {get; set;}
    }
}