using Core.Entities.VNVCModels;

namespace Core.Interfaces.VnvcInterfaces
{
    public interface IVnvcRDbRepository
    {
        Task<KHACH_HANG> GetNgMuaTheoCccdAsync (string cc);
        Task<KHACH_HANG> CreateNgMuaAsync (KHACH_HANG kh);
        Task<bool> UpdateNgMuaBySdt(string ngMuaCc, string ngMuaSdt);
    }
}