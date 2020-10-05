using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable
{
    /// <summary>
    /// Тесты добавления элементов в базе данных
    /// </summary>
    public static class BoutiqueEntityDatabaseAdding
    {
        /// <summary>
        /// Добавление сущности в таблицу
        /// </summary>
        public static async Task AddRange(IBoutiqueDatabase database, IReadOnlyCollection<CategoryEntity> categoryEntity)
        {
            var ids = await database.CategoryTable.AddRangeAsync(categoryEntity);

            Assert.True(ids.OkStatus);
            Assert.True(ids.Value.SequenceEqual(categoryEntity.Select(category => category.Id)));
        }
    }
}