using BoutiqueEF.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueEF.Mappings.Clothes
{
    public class GenderConfiguration : IEntityTypeConfiguration<GenderEntity>
    {
        public void Configure(EntityTypeBuilder<GenderEntity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(t => t.GenderType).IsRequired().HasConversion<string>();
            builder.Property(t => t.Name).IsRequired();
        }
    }
}