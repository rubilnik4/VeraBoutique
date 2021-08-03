using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping.Sequences;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Одежда. Информация. Схема базы данных
    /// </summary>
    public class ClothesConfiguration : IEntityTypeConfiguration<ClothesEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasDefaultValueSql(DatabaseSequence.ClothesSequence.SqlSequenceCommand).IsRequired();
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.Price).IsRequired();

            builder.HasOne(t => t.Gender)
                   .WithMany(s => s!.Clothes)
                   .HasForeignKey(sc => sc.GenderType)
                   .IsRequired();

            builder.HasOne(t => t.ClothesType)
                   .WithMany(s => s!.Clothes)
                   .HasForeignKey(sc => sc.ClothesTypeName)
                   .IsRequired();
        }
    }
}