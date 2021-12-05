using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Store
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        //Mã nhà cung cấp
        [ForeignKey("SupplierId")]
        public Supplier t_supplier {get; set;}
        //Địa chỉ
        [Column(TypeName = "nvarchar")]
        public string Address {get; set;}
        //Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}
    }
}