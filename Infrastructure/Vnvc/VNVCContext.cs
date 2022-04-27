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

        public DbSet<KhachHang> khachHang { get; set; }
        public DbSet<TinhThanh> tinhThanh { get; set; }
        public DbSet<Vip> vip { get; set; }
        public DbSet<DonHang> donHang { get; set; }
        public DbSet<ChiTietDonHang> chiTietDonHang { get; set; }
        public DbSet<ChiNhanh> chiNhanh { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        }
    }
}