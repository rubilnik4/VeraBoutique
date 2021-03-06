﻿using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => new { t.ClothesSizeType, t.SizeNormalize }).IsUnique();
            builder.Property(t => t.ClothesSizeType).IsRequired();
            builder.Property(t => t.SizeNormalize).IsRequired();
        }
    }
}