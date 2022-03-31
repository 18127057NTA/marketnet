using System.Reflection;
using Core.Entities.VNVCModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class VNVCContext : DbContext
    {
        public VNVCContext(DbContextOptions<VNVCContext> options) : base(options)
        {
        }

        public DbSet<KHACH_HANG> khachHang { get; set; }
        public DbSet<TINH_THANH> tinhThanh { get; set; }
        public DbSet<VIP> vips { get; set; }
        public DbSet<DON_HANG> donHang { get; set; }
        public DbSet<CHI_TIET_DON_HANG> cTietDonHang { get; set; }
        public DbSet<CHI_NHANH> chiNhanh { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        }
    }
}