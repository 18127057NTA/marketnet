using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BasketItemDto
    {
        //[Required]
        //public int Id {get; set;}
        public string Id {get; set;}
        //[Required]
        public string Ten {get; set;}
        //[Required]
        //[Range(500, 1300000, ErrorMessage = "Đơn giá phải từ 500 đồng trở lên ")]// có thể ghi tiếng Anh
        //public int UnitPrice {get; set;}
        public int Gia {get; set;}
        //[Required]
        //[Range(1, 1300000, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 0")]// có thể ghi tiếng Anh
        //public int Quantity {get; set;}
        public int SoLuongGoi {get; set;}
        //[Required]
        //public string PictureUrl {get; set;}
        public string HinhAnh {get; set;}

        /*[Required]
        public string StoreName {get; set;}
        [Required]
        public string StoreAddress {get; set;}
        [Required]
        public string TypeName {get; set;}
        [Required]
        public string SupplierName {get; set;}*/
    }
}