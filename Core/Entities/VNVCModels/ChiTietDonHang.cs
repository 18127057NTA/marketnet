namespace Core.Entities.VNVCModels
{
    public class ChiTietDonHang
    {
        public int Id {get; set;}
        public int DonGia {get; set;}
        public DonHang DonHang {get; set;}
        public int DonHangId {get; set;}
        public string MaSanPham {get; set;}
        public int SoLuong{get; set;}
        public string TenSanPham {get; set;}
    }
}