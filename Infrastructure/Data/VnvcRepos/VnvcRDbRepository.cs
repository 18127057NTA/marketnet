using Core.Entities.VNVCModels;
using Core.Interfaces.VnvcInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.VnvcRepos
{
    public class VnvcRDbRepository : IVnvcRDbRepository
    {
        private readonly VNVCContext _vnvcContxt;
        public VnvcRDbRepository(VNVCContext vnvcContext)
        {
            _vnvcContxt = vnvcContext;
        }
        public async Task<bool> CreateCTDHAsync(ChiTietDonHang ctdhang)
        {
            //Đơn hàng lúc tải vô chưa có id
            await _vnvcContxt.chiTietDonHang.AddAsync(ctdhang);
            return await _vnvcContxt.SaveChangesAsync() > 0;
        }

        public async Task<DonHang> CreateDonHangAsync(DonHang donHang)
        {

            await _vnvcContxt.donHang.AddAsync(donHang);
            await _vnvcContxt.SaveChangesAsync();
            //Có khả năng phải tạo biến mới copy donHang thì id ms nhận?
            return donHang;
        }

        public async Task<KhachHang> CreateNgMuaAsync(KhachHang kh)
        {
            var nguoiMua = await this.GetNgMuaTheoCccdAsync(kh.Cccd);
            if (nguoiMua != null && nguoiMua.Cccd != kh.Cccd)
            {
                await this.UpdateNgMuaBySdt(nguoiMua.Cccd, kh.Sdt);
                return nguoiMua;
            }
            await _vnvcContxt.khachHang.AddAsync(kh);
            await _vnvcContxt.SaveChangesAsync();
            return await this.GetNgMuaTheoCccdAsync(kh.Cccd);
        }

        public async Task<KhachHang> GetNgMuaTheoCccdAsync(string cc)
        {
            return await _vnvcContxt.khachHang.FirstOrDefaultAsync(kh => kh.Cccd == cc);
        }

        public async Task<Vip> GetValidVipAsync(string maVip)
        {
            var vip = await this.GetVipAsync(maVip);
            if(vip != null && vip.NgayBD.Date <= DateTime.Now.Date && vip.NgayKT.Date >= DateTime.Now.Date){
                return vip;
            }
            return null;
        }

        public async Task<Vip> GetVipAsync(string vipId)
        {
            //Lấy thời điểm gấn nhất
            return await _vnvcContxt.vip.OrderByDescending(v => v.Id).FirstOrDefaultAsync(v=> v.KhachHangMaVip == vipId);//OrderBy(v1 => v1.Id).LastOrDefaultAsync(v => v.KhachHangMaVip == vipId);
        }

        public async Task<bool> UpdateDonHangByTongTien(int maDonHang, int tongtien)
        {
            var dhCu = await _vnvcContxt.donHang.FirstOrDefaultAsync(dh => dh.Id == maDonHang);
            dhCu.TongTien = tongtien;
            
            _vnvcContxt.donHang.Attach(dhCu);
            _vnvcContxt.Entry(dhCu).State = EntityState.Modified;

            return await _vnvcContxt.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateNgMuaBySdt(string ngMuaCc, string ngMuaSdt)
        {
            var khCu = await _vnvcContxt.khachHang.FirstOrDefaultAsync(k1 => k1.Cccd == ngMuaCc);
            khCu.Sdt = ngMuaSdt;

            _vnvcContxt.khachHang.Attach(khCu);
            _vnvcContxt.Entry(khCu).State = EntityState.Modified;

            return await _vnvcContxt.SaveChangesAsync() > 0;
        }
    }
}