namespace Core.Entities.VNVCModels
{
    public class Vip
    {
        public int Id {get; set;}
        public KhachHang KhachHang {get; set;}
        public string KhachHangMaVip {get; set;}
        public DateTime NgayBD {get; set;}
        public DateTime NgayKT {get; set;}
    }
}