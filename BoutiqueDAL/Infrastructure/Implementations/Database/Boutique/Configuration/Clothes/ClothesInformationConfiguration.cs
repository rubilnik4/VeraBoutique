using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping;
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
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasDefaultValueSql($"nextval('\"{ClothesSequences.CLOTHES_ID_GENERATOR}\"')").IsRequired();
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Price).IsRequired();
            builder.Property(t => t.Image).IsRequired();

            builder.HasOne(t => t.ClothesTypeEntity)
                   .WithMany(s => s!.ClothesInformationEntities)
                   .HasForeignKey(sc => sc.ClothesTypeName)
                   .IsRequired();
        }
    }
}