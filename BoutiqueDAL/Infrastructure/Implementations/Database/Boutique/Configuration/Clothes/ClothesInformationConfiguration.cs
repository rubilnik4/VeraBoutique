using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Одежда. Информация. Схема базы данных
    /// </summary>
    public class ClothesInformationConfiguration : IEntityTypeConfiguration<ClothesInformationEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesInformationEntity> builder)
        {
            builder.HasKey(t => t.Generated);
            builder.Property(t => t.Generated).IsRequired().HasDefaultValueSql("nextval('\"OrderNumbers\"')");
           //
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Price).IsRequired();
            builder.Property(t => t.Image).IsRequired();
        }
    }
}