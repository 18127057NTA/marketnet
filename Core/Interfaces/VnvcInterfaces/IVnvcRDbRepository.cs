using Core.Entities.VNVCModels;

namespace Core.Interfaces.VnvcInterfaces
{
    public interface IVnvcRDbRepository
    {
        
        Task<KhachHang> GetNgMuaTheoCccdAsync (string cc);
        Task<KhachHang> CreateNgMuaAsync (KhachHang kh);
        Task<bool> UpdateNgMuaBySdt(string ngMuaCc, string ngMuaSdt);
        Task<DonHang> CreateDonHangAsync(DonHang donHang);
        Task<bool> CreateCTDHAsync(ChiTietDonHang ctdhang);
        Task<bool> UpdateDonHangByTongTien(int maDonHang, int tongtien);
        Task<Vip> GetValidVipAsync(string maVip);
        Task<Vip> GetVipAsync(string vipId);
    }
}