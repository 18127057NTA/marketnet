
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ProductType: BaseEntity // Mới thêm
    {
        //public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }
        /*//Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}*/
    }
}