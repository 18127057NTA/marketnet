using Core.Entities.VNVCModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.VnvcConfig
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            //builder.Property(k => k.Id).IsRequired();
        }
    }
}