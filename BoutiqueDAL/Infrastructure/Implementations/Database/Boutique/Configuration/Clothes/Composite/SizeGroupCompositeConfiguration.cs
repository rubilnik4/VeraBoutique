using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность размера одежды с группой. Схема базы данных
    /// </summary>
    public class SizeGroupCompositeConfiguration : IEntityTypeConfiguration<SizeGroupCompositeEntity>
    {
        public void Configure(EntityTypeBuilder<SizeGroupCompositeEntity> builder)
        {
            builder.HasKey(t => new { t.SizeType, t.SizeName, t.SizeGroupId });

            builder.HasOne(t => t.Size)
                   .WithMany(s => s!.SizeGroupComposites)
                   .HasForeignKey(sc => new { sc.SizeType, sc.SizeName })
                   .IsRequired();

            builder.HasOne(t => t.SizeGroup)
                   .WithMany(s => s!.SizeGroupComposites)
                   .HasForeignKey(sc => sc.SizeGroupId)
                   .IsRequired();
        }
    }
}