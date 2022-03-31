using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class TINH_THANH
    {
        //public int TT_ID {get; set;}
        //Id
        [Key]
        [Column(TypeName = "nvarchar")]
        public string TT_ID {get; set;}
        //Tên tỉnh thành
        [Column(TypeName = "nvarchar")]
        public string TT_TENTT {get; set;}

    }
}