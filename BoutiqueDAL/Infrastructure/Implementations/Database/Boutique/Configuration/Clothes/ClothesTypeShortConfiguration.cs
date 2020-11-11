using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Вид одежды. Базовые данные. Схема базы данных
    /// </summary>
    public class ClothesTypeShortConfiguration : IEntityTypeConfiguration<ClothesTypeShortEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesTypeShortEntity> builder)
        {
            builder.HasKey(t => t.Name);

            builder.HasOne(t => t.Category)
                   .WithMany(s => s!.ClothesTypes)
                   .HasForeignKey(sc => sc.CategoryName)
                   .IsRequired();
        }
    }
}