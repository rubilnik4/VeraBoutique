using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Размер одежды. Схема базы данных
    /// </summary>
    public class ClothesSizeConfiguration : IEntityTypeConfiguration<ClothesSizeEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesSizeEntity> builder)
        {
            builder.HasKey(t => new { ClothesSizeType = t.SizeType, Size = t.SizeValue });
            builder.Property(t => new {ClothesSizeType = t.SizeType}).IsRequired();
            builder.Property(t => new {Size = t.SizeValue}).IsRequired();
            builder.Property(t => new { t.SizeName }).IsRequired();
        }
    }
}