using Api_EntityConfiguration_Validator_Dtos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_EntityConfiguration_Validator_Dtos.DAL.Configurations
{
    public class ProductModelConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(n => n.Name).HasMaxLength(100);
            builder.Property(n => n.CostPrice).HasColumnType("decimal(18,2)");
            builder.Property(n => n.SalePrice).HasColumnType("decimal(18,2)");
            builder.Property(n => n.IsDelete).HasDefaultValue(false);
            builder.Property(n => n.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.Property(n => n.UpdatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}
