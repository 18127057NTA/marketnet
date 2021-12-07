using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Store: BaseEntity // Mới thêm
    {
        //public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        //Mã nhà cung cấp
        //[ForeignKey("SupplierId")]
        public Supplier Supplier {get; set;} // lúc đầu t_supplier
        public int SupplierId {get; set;}
        //Địa chỉ
        [Column(TypeName = "nvarchar")]
        public string Address {get; set;}
        /*//Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}*/
    }
}