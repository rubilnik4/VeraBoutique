using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность одежды с размером. Схема базы данных
    /// </summary>
    public class ClothesSizeGroupCompositeConfiguration : IEntityTypeConfiguration<ClothesSizeGroupCompositeEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesSizeGroupCompositeEntity> builder)
        {
            builder.HasKey(t => new { t.ClothesId, t.SizeGroupId });

            builder.HasOne(t => t.Clothes)
                   .WithMany(s => s!.ClothesSizeGroupComposites)
                   .HasForeignKey(sc => sc.ClothesId)
                   .IsRequired();

            builder.HasOne(t => t.SizeGroup)
                   .WithMany(s => s!.ClothesSizeGroupComposites)
                   .HasForeignKey(sc => sc.SizeGroupId)
                   .IsRequired();
        }
    }
}