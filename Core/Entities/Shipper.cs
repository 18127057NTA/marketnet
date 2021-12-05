using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Shipper
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
        //Bằng lái xe
        [Column(TypeName = "nvarchar")]
        public string DriveLicense {get; set;}
        //Giấy tiêm
        [Column(TypeName = "nvarchar")]
        public string VaccinatedConfirmed {get; set;}
        //Ngày tạo
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate {get; set;}
        //Ngày cập nhật
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate {get; set;}
    }
}