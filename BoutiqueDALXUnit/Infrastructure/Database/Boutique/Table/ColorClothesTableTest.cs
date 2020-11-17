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
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateValueFilter()
        {
            var colorClothesDomain = ColorClothesData.ColorClothesDomain.First();
            var colorsClothes = ColorClothesEntityData.ColorClothesEntities.AsQueryable();
            var colorClothesTable = ColorClothesTable;
            var colorClothesEntityConverter = ColorClothesEntityConverterMock.ColorClothesEntityConverter;

            var entities = colorClothesTable.ValidateFilter(colorsClothes, colorClothesDomain);
            var domains = colorClothesEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(1, domains.Value.Count);
            Assert.True(colorClothesDomain.Equals(domains.Value.First()));
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateCollectionFilter()
        {
            var colorClothesDomains = ColorClothesData.ColorClothesDomain;
            var colorsClothes = ColorClothesEntityData.ColorClothesEntities.AsQueryable();
            var colorClothesTable = ColorClothesTable;
            var colorClothesEntityConverter = ColorClothesEntityConverterMock.ColorClothesEntityConverter;

            var entities = colorClothesTable.ValidateFilter(colorsClothes, colorClothesDomains);
            var domains = colorClothesEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(colorClothesDomains.Count, domains.Value.Count);
            Assert.True(colorClothesDomains.SequenceEqual(domains.Value));
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