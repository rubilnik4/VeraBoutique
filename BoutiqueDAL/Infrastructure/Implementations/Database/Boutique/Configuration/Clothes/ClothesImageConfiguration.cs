using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping.Sequences;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Изображения. Схема базы данных
    /// </summary>
    public class ClothesImageConfiguration : IEntityTypeConfiguration<ClothesImageEntity>
    {
        public void Configure(EntityTypeBuilder<ClothesImageEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasDefaultValueSql(DatabaseSequence.ClothesImageSequence.SqlSequenceCommand).IsRequired();
            builder.Property(t => t.Image).IsRequired();
            builder.Property(t => t.IsMain).IsRequired();

            builder.HasOne(t => t.Clothes)
                   .WithMany(s => s!.ClothesImages)
                   .HasForeignKey(sc => sc.ClothesId)
                   .IsRequired();
        }
    }
}