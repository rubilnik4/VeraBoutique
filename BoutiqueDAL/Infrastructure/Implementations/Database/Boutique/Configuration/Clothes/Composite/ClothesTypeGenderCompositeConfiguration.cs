using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность пола и вида одежды. Схема базы данных
    /// </summary>
    public class ClothesTypeGenderCompositeConfiguration : IEntityTypeConfiguration<ClothesTypeGenderCompositeEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesTypeGenderCompositeEntity> builder)
        {
            builder.HasKey(t => new { t.ClothesType, t.GenderType });

            builder.HasOne(t => t.Gender)
                   .WithMany(s => s!.ClothesTypeGenderComposites)
                   .HasForeignKey(sc => sc.GenderType)
                   .IsRequired();

            builder.HasOne(t => t.ClothesType)
                   .WithMany(s => s!.ClothesTypeGenderComposites)
                   .HasForeignKey(sc => sc.ClothesType)
                   .IsRequired();
        }
    }
}