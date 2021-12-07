using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Order: BaseEntity // Mới thêm
    {
        //public int Id { get; set; }

        //Mã người mua
        //[ForeignKey("BuyerId")]
        public Buyer Buyer {get; set;} //t_buyer
        public int BuyerId {get; set;}
        
        //Địa chỉ giao hàng
        [Column(TypeName = "nvarchar")]
        public string Address {get; set;}
        //Tổng tiền
        public int TotalCost {get; set;}
        /*//Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}*/
    }
}