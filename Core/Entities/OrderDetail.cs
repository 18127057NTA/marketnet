using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrderDetail: BaseEntity // Mới thêm
    {
        //public int Id { get; set; }

        //Mã đơn hàng
        //[ForeignKey("OrderId")]
        public Order Order {get; set;} // lúc đầu t_order
        public int OrderId {get; set;}
        
        //Mã sản phẩm
        //[ForeignKey("ProductId")]
        public Product Product {get; set;} // lúc đầu t_product
        public int ProductId {get; set;}
        //Số lượng
        public int Quantity {get; set;}
        /*//Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}*/
    }
}