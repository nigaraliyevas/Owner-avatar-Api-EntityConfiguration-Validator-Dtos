using Api_EntityConfiguration_Validator_Dtos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_EntityConfiguration_Validator_Dtos.DAL.Configurations
{
    public class CategoryModelConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(n => n.IsDelete).HasDefaultValue(false);
            builder.Property(n => n.CreatedDate).HasDefaultValue(DateTime.Now);
            builder.Property(n => n.UpdatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}
