using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Вид одежды.Схема базы данных
    /// </summary>
    public class ClothesTypeConfiguration : IEntityTypeConfiguration<ClothesTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesTypeEntity> builder)
        {
            builder.HasKey(t => t.Name);
        }
    }
}