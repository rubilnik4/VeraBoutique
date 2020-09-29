using BoutiqueDALXUnit.Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Включенная тестовая сущность. Схема базы данных
    /// </summary>
    public class TestIncludeConfiguration : IEntityTypeConfiguration<TestIncludeEntity>
    {
        public void Configure(EntityTypeBuilder<TestIncludeEntity> builder)
        {
            builder.HasKey(t => t.Name);

            builder.HasOne(t => t.TestEntity).
                    WithMany(s => s!.TestIncludeEntities).
                    HasForeignKey(sc => sc.TestId);
        }
    }
}