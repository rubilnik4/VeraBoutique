using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность пола и вида одежды. Схема базы данных
    /// </summary>
    public class GenderCategoryCompositeConfiguration : IEntityTypeConfiguration<GenderCategoryCompositeEntity>
    {
        public void Configure(EntityTypeBuilder<GenderCategoryCompositeEntity> builder)
        {
            builder.HasKey(t => new { GenderId = t.GenderType, CategoryId = t.CategoryName });

            builder.HasOne(t => t.Gender)
                   .WithMany(s => s!.GenderCategoryComposites)
                   .HasForeignKey(sc => sc.GenderType)
                   .IsRequired();

            builder.HasOne(t => t.Category)
                   .WithMany(s => s!.GenderCategoryComposites)
                   .HasForeignKey(sc => sc.CategoryName)
                   .IsRequired();
        }
    }
}