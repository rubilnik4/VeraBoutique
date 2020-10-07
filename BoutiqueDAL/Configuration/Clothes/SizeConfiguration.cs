using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Размер одежды. Схема базы данных
    /// </summary>
    public class SizeConfiguration : IEntityTypeConfiguration<SizeEntity>
    {
        public void Configure(EntityTypeBuilder<SizeEntity> builder)
        {
            builder.HasKey(t => new {t.SizeType, t.SizeValue });
            builder.Property(t => t.SizeType).IsRequired();
            builder.Property(t => t.SizeValue).IsRequired();
            builder.Property(t => t.SizeName).IsRequired();

            builder.HasOne(t => t.SizeGroupEntity)
                   .WithMany(s => s!.Sizes)
                   .HasForeignKey(sc => new { sc.ClothesSizeTypeId, sc.SizeNormalizeId })
                   .IsRequired();
        }
    }
}