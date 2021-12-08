using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Размер одежды. Схема базы данных
    /// </summary>
    public class SizeConfiguration : IEntityTypeConfiguration<SizeEntity>
    {
        public void Configure(EntityTypeBuilder<SizeEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => new { t.SizeType, t.Name }).IsUnique();
            builder.Property(t => t.SizeType).IsRequired();
            builder.Property(t => t.Name).IsRequired();
        }
    }
}