using BoutiqueDAL.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDAL.Configuration.Clothes
{
    /// <summary>
    /// Пол. Схема базы данных
    /// </summary>
    public class GenderConfiguration : IEntityTypeConfiguration<GenderEntity>
    {
        public void Configure(EntityTypeBuilder<GenderEntity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(t => t.GenderType).IsRequired();
            builder.Property(t => t.Name).IsRequired();
        }
    }
}