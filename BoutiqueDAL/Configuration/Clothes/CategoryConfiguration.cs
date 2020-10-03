using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Категория одежды. Схема базы данных
    /// </summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(t => t.Name);

            //builder.HasMany(t => t.ClothesTypeEntities)
            //       .WithOne(s => s.CategoryEntity!)
            //       .HasForeignKey(sc => sc.CategoryEntityId)
            //       .IsRequired();
        }
    }
}