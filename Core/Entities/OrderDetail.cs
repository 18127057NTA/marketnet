using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        //Mã đơn hàng
        [ForeignKey("OrderId")]
        public Order t_order {get; set;}
        
        //Mã sản phẩm
        [ForeignKey("ProductId")]
        public Product t_product {get; set;} 
        //Số lượng
        public int Quantity {get; set;}
        //Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}
    }
}