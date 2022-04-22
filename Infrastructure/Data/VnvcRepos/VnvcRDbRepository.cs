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

        public async Task<bool> CreateCTDHAsync(CHI_TIET_DON_HANG ctdhang)
        {
            //Đơn hàng lúc tải vô chưa có id
            await _vnvcContxt.cTietDonHang.AddAsync(ctdhang);
            return await _vnvcContxt.SaveChangesAsync() > 0;
        }

        public async Task<DON_HANG> CreateDonHangAsync(DON_HANG donHang)
        {
            await _vnvcContxt.donHang.AddAsync(donHang);
            await _vnvcContxt.SaveChangesAsync();
            //Có khả năng phải tạo biến mới copy donHang thì id ms nhận?
            return donHang;
        }

        public async Task<KHACH_HANG> CreateNgMuaAsync(KHACH_HANG kh)
        {
            var nguoiMua = await this.GetNgMuaTheoCccdAsync(kh.KH_CCCD);
            if (nguoiMua != null && nguoiMua.KH_SDT != kh.KH_SDT)
            {
                await this.UpdateNgMuaBySdt(nguoiMua.KH_CCCD, kh.KH_SDT);
                return nguoiMua;
            }
            await _vnvcContxt.khachHang.AddAsync(kh);
            await _vnvcContxt.SaveChangesAsync();
            return await this.GetNgMuaTheoCccdAsync(kh.KH_CCCD);
        }

        public async Task<KHACH_HANG> GetNgMuaTheoCccdAsync(string cc)
        {
            return await _vnvcContxt.khachHang.FirstOrDefaultAsync(kh => kh.KH_CCCD == cc);
        }

        public async Task<bool> UpdateNgMuaBySdt(string ngMuaCc, string ngMuaSdt)
        {
            var khCu = await _vnvcContxt.khachHang.FirstOrDefaultAsync(k1 => k1.KH_CCCD == ngMuaCc);
            khCu.KH_SDT = ngMuaSdt;

            _vnvcContxt.khachHang.Attach(khCu);
            _vnvcContxt.Entry(khCu).State = EntityState.Modified;

            return _vnvcContxt.SaveChanges() > 0;
        }
    }
}