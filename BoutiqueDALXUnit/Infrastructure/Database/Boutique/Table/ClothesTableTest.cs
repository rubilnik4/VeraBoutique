using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных одежды. Тесты
    /// </summary>
    public class ClothesTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesTable = ClothesTable;

            var id = clothesTable.IdSelect().Compile()(clothes);

            Assert.Equal(clothes.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesTable = ClothesTable;

            bool isFound = clothesTable.IdPredicate(clothes.Id).Compile()(clothes);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var clothes = ClothesEntitiesData.ClothesEntities;
            var clothesTable = ClothesTable;

            bool isFound = clothesTable.IdsPredicate(clothes.Select(category => category.Id)).
                                            Compile()(clothes.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateValueFilter()
        {
            var clothesDomain = ClothesData.ClothesDomains.First();
            var clothes = ClothesEntitiesData.ClothesEntities.AsQueryable();
            var clothesTable = ClothesTable;
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesEntityConverter;

            var entities = clothesTable.ValidateFilter(clothes, clothesDomain);
            var domains = clothesEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(1, domains.Value.Count);
            Assert.True(clothesDomain.Equals(domains.Value.First()));
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateCollectionFilter()
        {
            var clothesDomains = ClothesData.ClothesDomains;
            var clothes = ClothesEntitiesData.ClothesEntities.AsQueryable();
            var clothesTable = ClothesTable;
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesEntityConverter;

            var entities = clothesTable.ValidateFilter(clothes, clothesDomains);
            var domains = clothesEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(clothesDomains.Count, domains.Value.Count);
            Assert.True(clothesDomains.SequenceEqual(domains.Value));
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<ClothesEntity>> DbSet =>
            new Mock<DbSet<ClothesEntity>>();

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private static IClothesTable ClothesTable =>
            new ClothesTable(DbSet.Object);
    }
}