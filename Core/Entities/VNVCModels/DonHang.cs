namespace Core.Entities.VNVCModels
{
    public class DonHang
    {
        public int Id {get; set;}
        public ChiNhanh ChiNhanh {get; set;}
        public string ChiNhanhId {get; set;}
        public KhachHang KhachHang {get; set;}
        public int KhachHangId {get; set;}
        public DateTime NgayMua {get; set;}
        public DateTime NgayTiem {get; set;}
        public int TongTien {get; set;}
        public string TinhTrang {get; set;}
    }
}