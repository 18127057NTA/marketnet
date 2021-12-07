using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.Property(p => p.Id).IsRequired(); // nên bỏ để thả lỏng
            //builder.Property(p => p.Name).IsRequired(); // nên bỏ để thả lỏng

            builder.HasOne(t => t.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);

            builder.HasOne(st => st.Store).WithMany()
                .HasForeignKey(p => p.StoreId);

            //builder.Property(p => p.CreatedDate).HasColumnType("datetime");
        }
    }
}