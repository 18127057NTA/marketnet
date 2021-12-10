using System.ComponentModel.DataAnnotations.Schema;


namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id {get; set;}    
        public string Name { get; set; }
        //Mã loại sản phẩm 
        //[ForeignKey("TypeId")]
        //public ProductType ProductType {get; set;} // lúc đầu t_type
        public string ProductType {get; set;}
        public int ProductTypeId {get; set;}
        //Mã cửa hàng
        //[ForeignKey("StoreId")]
        //public Store Store {get; set;} // lúc đầu t_store
        public string Store {get; set;} // Còn những thông tin phụ khác + supplier thì sao ?
        public int StoreId {get; set;}// Có thể lược bỏ nếu ko cần

        public string StoreAddress {get; set;}// Chú ý thêm bớt hợp đề bài

        public int SupplierId {get; set;} // Chú ý thêm bớt hợp đề bài
        public string SupplierName {get; set;} // Chú ý thêm bớt hợp đề bài
        //Số lượng
        public int Quantity {get; set;}
        //Đơn giá
        public int UnitPrice {get; set;}
        //Hình ảnh
        public string PictureUrl {get; set;}
    }
}