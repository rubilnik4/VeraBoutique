using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table.Clothes
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
            var colorClothes = ColorEntityData.ColorEntities.First();
            var colorClothesTable = ColorTable;

            var id = colorClothesTable.IdSelect().Compile()(colorClothes);

            Assert.Equal(colorClothes.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var colorClothes = ColorEntityData.ColorEntities.First();
            var colorClothesTable = ColorTable;

            bool isFound = colorClothesTable.IdPredicate(colorClothes.Id).Compile()(colorClothes);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var colorsClothes = ColorEntityData.ColorEntities;
            var colorClothesTable = ColorTable;

            bool isFound = colorClothesTable.IdsPredicate(colorsClothes.Select(category => category.Id)).
                                             Compile()(colorsClothes.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<ColorEntity>> DbSet =>
            new Mock<DbSet<ColorEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static IColorTable ColorTable =>
            new ColorTable(DbSet.Object);
    }
}