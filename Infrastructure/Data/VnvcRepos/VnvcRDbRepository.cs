using Core.Entities.VNVCModels;
using Core.Interfaces.VnvcInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.VnvcRepos
{
    public class VnvcRDbRepository: IVnvcRDbRepository
    {
        private readonly VNVCContext _vnvcContxt;
        public VnvcRDbRepository(VNVCContext vnvcContext){
            _vnvcContxt = vnvcContext;
        }

        public async Task<KHACH_HANG> CreateNgMuaAsync(KHACH_HANG kh)
        {
            await _vnvcContxt.khachHang.AddAsync(kh);
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