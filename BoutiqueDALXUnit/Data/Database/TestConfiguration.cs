using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDALXUnit.Data.Database
{
    /// <summary>
    /// Пол. Схема базы данных
    /// </summary>
    public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasKey(t => t.TestEnum);
            builder.Property(t => t.Name).IsRequired();
        }
    }
}