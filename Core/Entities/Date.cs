using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Date
    {
        [Column(TypeName = "datetime")]
        public string CreatedDate {get; set;}
        
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public string UpdatedDate {get; set;}
    }
}