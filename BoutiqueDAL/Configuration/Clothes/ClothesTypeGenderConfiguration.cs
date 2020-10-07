using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Связующая сущность пола и вида одежды. Схема базы данных
    /// </summary>
    public class ClothesTypeGenderConfiguration : IEntityTypeConfiguration<ClothesTypeGenderEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesTypeGenderEntity> builder)
        {
            builder.HasKey(t => new { t.ClothesType, t.GenderType });

            builder.HasOne(t => t.GenderEntity)
                   .WithMany(s => s!.ClothesTypeGenderEntities)
                   .HasForeignKey(sc => sc.GenderType)
                   .IsRequired();

            builder.HasOne(t => t.ClothesTypeEntity)
                   .WithMany(s => s!.ClothesTypeGenderEntities)
                   .HasForeignKey(sc => sc.ClothesType)
                   .IsRequired();
        }
    }
}