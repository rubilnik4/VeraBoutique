using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность одежды с цветом. Схема базы данных
    /// </summary>
    public class ClothesColorCompositeConfiguration : IEntityTypeConfiguration<ClothesColorCompositeEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesColorCompositeEntity> builder)
        {
            builder.HasKey(t => new { t.ClothesId, t.ColorName });

            builder.HasOne(t => t.ClothesInformationEntity)
                   .WithMany(s => s!.ClothesColorCompositeEntities)
                   .HasForeignKey(sc => sc.ClothesId)
                   .IsRequired();

            builder.HasOne(t => t.ColorClothesEntity)
                   .WithMany(s => s!.ClothesColorCompositeEntities)
                   .HasForeignKey(sc => sc.ColorName)
                   .IsRequired();
        }
    }
}