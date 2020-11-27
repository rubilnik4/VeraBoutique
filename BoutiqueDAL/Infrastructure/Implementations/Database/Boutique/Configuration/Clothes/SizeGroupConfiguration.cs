using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Схема базы данных
    /// </summary>
    public class SizeGroupConfiguration : IEntityTypeConfiguration<SizeGroupEntity>
    {
        public void Configure(EntityTypeBuilder<SizeGroupEntity> builder)
        {
            builder.HasKey(t => new { t.ClothesSizeType, t.SizeNormalize });
            builder.Property(t => t.ClothesSizeType).IsRequired();
            builder.Property(t => t.SizeNormalize).IsRequired();
        }
    }
}