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
            if (nguoiMua != null && nguoiMua.Sdt != kh.Cccd)
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

        public async Task<bool> UpdateNgMuaBySdt(string ngMuaCc, string ngMuaSdt)
        {
            var khCu = await _vnvcContxt.khachHang.FirstOrDefaultAsync(k1 => k1.Cccd == ngMuaCc);
            khCu.Sdt = ngMuaSdt;

            _vnvcContxt.khachHang.Attach(khCu);
            _vnvcContxt.Entry(khCu).State = EntityState.Modified;

            return _vnvcContxt.SaveChanges() > 0;
        }
    }
}