﻿using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Связующая сущность размера одежды с группой. Схема базы данных
    /// </summary>
    public class SizeGroupCompositeConfiguration : IEntityTypeConfiguration<SizeGroupCompositeEntity>
    {
        public void Configure(EntityTypeBuilder<SizeGroupCompositeEntity> builder)
        {
            builder.HasKey(t => new { t.SizeType, t.SizeName, t.ClothesSizeType, t.SizeNormalize });

            builder.HasOne(t => t.SizeEntity)
                   .WithMany(s => s!.SizeGroupCompositeEntities)
                   .HasForeignKey(sc => new { sc.SizeType, sc.SizeName })
                   .IsRequired();

            builder.HasOne(t => t.SizeGroupEntity)
                   .WithMany(s => s!.SizeGroupCompositeEntities)
                   .HasForeignKey(sc => new { sc.ClothesSizeType, sc.SizeNormalize })
                   .IsRequired();
        }
    }
}