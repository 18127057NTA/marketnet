using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Product: BaseEntity // Mới thêm Base Entity
    {
        //public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }
        //Mã loại sản phẩm 
        //[ForeignKey("TypeId")]
        public ProductType ProductType {get; set;} // lúc đầu t_type
        public int ProductTypeId {get; set;}
        //Mã cửa hàng
        //[ForeignKey("StoreId")]
        public Store Store {get; set;} // lúc đầu t_store
        public int StoreId {get; set;}
        //Số lượng
        public int Quantity {get; set;}
        //Đơn giá
        public int UnitPrice {get; set;}
        //Hình ảnh
        public string PictureUrl {get; set;}
        /*//Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}*/
    }
}