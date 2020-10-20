using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes
{
    /// <summary>
    /// Пол. Схема базы данных
    /// </summary>
    public class GenderConfiguration : IEntityTypeConfiguration<GenderEntity>
    {
        public void Configure(EntityTypeBuilder<GenderEntity> builder)
        {
            builder.HasKey(t => t.GenderType);
            builder.Property(t => t.Name).IsRequired();
        }
    }
}