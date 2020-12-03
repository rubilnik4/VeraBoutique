using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных категорий одежды. Тесты
    /// </summary>
    public class CategoryTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var category = CategoryEntitiesData.CategoryEntities.First();
            var categoryTable = CategoryTable;

            var id = categoryTable.IdSelect().Compile()(category);

            Assert.Equal(category.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var category = CategoryEntitiesData.CategoryEntities.First();
            var categoryTable = CategoryTable;

            bool isFound = categoryTable.IdPredicate(category.Id).Compile()(category);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var categories = CategoryEntitiesData.CategoryEntities;
            var categoryTable = CategoryTable;

            bool isFound = categoryTable.IdsPredicate(categories.Select(category => category.Id)).
                                         Compile()(categories.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<CategoryEntity>> DbSet =>
            new Mock<DbSet<CategoryEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static ICategoryTable CategoryTable =>
            new CategoryTable(DbSet.Object);
    }
}