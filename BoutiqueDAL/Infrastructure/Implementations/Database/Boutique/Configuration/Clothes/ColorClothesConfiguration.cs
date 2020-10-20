using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Цвет одежды. Схема базы данных
    /// </summary>
    public class ColorClothesConfiguration : IEntityTypeConfiguration<ColorClothesEntity>
    {
        public void Configure(EntityTypeBuilder<ColorClothesEntity> builder)
        {
            builder.HasKey(t => t.Name);
        }
    }
}