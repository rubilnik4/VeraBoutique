using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных вида одежды. Тесты
    /// </summary>
    public class ClothesTypeTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var clothesType = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeTable = ClothesTypeTable;

            var id = clothesTypeTable.IdSelect().Compile()(clothesType);

            Assert.Equal(clothesType.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var clothesType = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeTable = ClothesTypeTable;

            bool isFound = clothesTypeTable.IdPredicate(clothesType.Id).Compile()(clothesType);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var clothesTypes = ClothesTypeEntitiesData.ClothesTypeEntities;
            var clothesTypeTable = ClothesTypeTable;

            bool isFound = clothesTypeTable.IdsPredicate(clothesTypes.Select(category => category.Id)).
                                            Compile()(clothesTypes.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateValueFilter()
        {
            var clothesTypeDomain = ClothesTypeData.ClothesTypeDomain.First();
            var clothesTypes = ClothesTypeEntitiesData.ClothesTypeEntities.AsQueryable();
            var clothesTypeTable = ClothesTypeTable;
            var clothesTypeEntityConverter = ClothesTypeEntityConverterMock.ClothesTypeEntityConverter;

            var entities = clothesTypeTable.ValidateFilter(clothesTypes, clothesTypeDomain);
            var domains = clothesTypeEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(1, domains.Value.Count);
            Assert.True(clothesTypeDomain.Equals(domains.Value.First()));
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateCollectionFilter()
        {
            var clothesTypeDomains = ClothesTypeData.ClothesTypeDomain;
            var clothesTypes = ClothesTypeEntitiesData.ClothesTypeEntities.AsQueryable();
            var clothesTypeTable = ClothesTypeTable;
            var clothesTypeEntityConverter = ClothesTypeEntityConverterMock.ClothesTypeEntityConverter;

            var entities = clothesTypeTable.ValidateFilter(clothesTypes, clothesTypeDomains);
            var domains = clothesTypeEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(clothesTypeDomains.Count, domains.Value.Count);
            Assert.True(clothesTypeDomains.SequenceEqual(domains.Value));
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<ClothesTypeEntity>> DbSet =>
            new Mock<DbSet<ClothesTypeEntity>>();

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private static IClothesTypeTable ClothesTypeTable =>
            new ClothesTypeTable(DbSet.Object);
    }
}