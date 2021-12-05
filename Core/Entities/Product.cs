using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        //Mã loại sản phẩm 
        [ForeignKey("TypeId")]
        public ProductType t_type {get; set;}
        //Mã cửa hàng
        [ForeignKey("StoreId")]
        public Store t_storeid {get; set;}
        //Số lượng
        public int Quantity {get; set;}
        //Đơn giá
        public int UnitPrice {get; set;}
        //Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}
    }
}