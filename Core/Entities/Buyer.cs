using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Buyer
    {
        public int Id { get; set; }
        

        [Column(TypeName = "nvarchar")]
        public string Name { get; set; }

        //Email
        [Column(TypeName = "nvarchar")]
        public string Email {get; set;}

        //Phone
        [Column(TypeName = "nvarchar")]
        public string Phone {get; set;}
        
        //Mật khẩu
        [Column(TypeName = "nvarchar")]
        public string Password {get; set;} //hashed
        
        //Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}
    }
}