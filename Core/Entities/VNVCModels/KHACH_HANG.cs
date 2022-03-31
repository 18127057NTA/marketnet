using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.VNVCModels
{
    public class KHACH_HANG
    {
        public int Id { get; set; }
        //Mã vip - một khách hàng có nhiều lần đăng ký vip
        [Column(TypeName = "nvarchar")]
        public string KH_ID { get; set; }
        //Căn cước công dân
        [Column(TypeName = "nvarchar")]
        public string KH_CCCD { get; set; }
        //Họ tên
        [Column(TypeName = "nvarchar")]
        public string KH_HOTEN { get; set; }
        //Email
        [Column(TypeName = "nvarchar")]
        public string KH_EMAIL { get; set; }
        // Số điện thoại
        [Column(TypeName = "nvarchar")]
        public string KH_SDT { get; set; }
        //Địa chỉ
        [Column(TypeName = "nvarchar")]
        public string KH_DIACHI { get; set; }
    }
}