using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Вид одежды.Схема базы данных
    /// </summary>
    public class ClothesTypeConfiguration : IEntityTypeConfiguration<ClothesTypeFullEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesTypeFullEntity> builder)
        {
            builder.HasKey(t => t.Name);

            builder.HasOne(t => t.Category)
                   .WithMany(s => s!.ClothesTypes)
                   .HasForeignKey(sc => sc.CategoryName)
                   .IsRequired();
        }
    }
}