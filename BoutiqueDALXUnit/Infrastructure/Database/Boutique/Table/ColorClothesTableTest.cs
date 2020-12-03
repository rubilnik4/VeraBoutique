using System.Linq;
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
    /// Таблица базы данных цвета одежды. Тест
    /// </summary>
    public class ColorClothesTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var colorClothes = ColorClothesEntityData.ColorClothesEntities.First();
            var colorClothesTable = ColorClothesTable;

            var id = colorClothesTable.IdSelect().Compile()(colorClothes);

            Assert.Equal(colorClothes.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var colorClothes = ColorClothesEntityData.ColorClothesEntities.First();
            var colorClothesTable = ColorClothesTable;

            bool isFound = colorClothesTable.IdPredicate(colorClothes.Id).Compile()(colorClothes);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var colorsClothes = ColorClothesEntityData.ColorClothesEntities;
            var colorClothesTable = ColorClothesTable;

            bool isFound = colorClothesTable.IdsPredicate(colorsClothes.Select(category => category.Id)).
                                             Compile()(colorsClothes.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<ColorClothesEntity>> DbSet =>
            new Mock<DbSet<ColorClothesEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static IColorClothesTable ColorClothesTable =>
            new ColorClothesTable(DbSet.Object);
    }
}