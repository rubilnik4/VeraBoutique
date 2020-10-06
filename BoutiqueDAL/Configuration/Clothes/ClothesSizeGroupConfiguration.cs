using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Схема базы данных
    /// </summary>
    public class ClothesSizeGroupConfiguration : IEntityTypeConfiguration<ClothesSizeGroupEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesSizeGroupEntity> builder)
        {
            builder.HasKey(t => new { t.ClothesSizeType, t.Size });
            builder.Property(t => new { t.ClothesSizeType }).IsRequired();
            builder.Property(t => new { t.Size }).IsRequired();
            builder.Property(t => new { t.SizeName }).IsRequired();
        }
    }
}