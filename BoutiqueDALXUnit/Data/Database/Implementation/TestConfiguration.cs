using BoutiqueDALXUnit.Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Тестовая сущность. Схема базы данных
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