using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class CHI_TIET_DON_HANG
    {
        //Khóa ngoại Đơn hàng
        public DON_HANG DON_HANG {get; set;}
        public int CTDH_IDDH {get; set;}
        //Khóa chính
        [Key]
        public int CTDH_ID {get; set;}
        //Mã sản phẩm bên mongo
        [Column(TypeName = "nvarchar")]
        public string CTDH_IDSP {get; set;}
        //Tên sản phẩm bên mongo
        [Column(TypeName = "nvarchar")]
        public string CTDH_TENSP {get; set;}
        //Giá sản phẩm bên mongo
        public int CTDH_GIA {get; set;}
        //Số lượng sản phẩm bên mongo
        public int CTDH_SL {get; set;}


    }
}