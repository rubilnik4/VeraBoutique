using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных размеров одежды. Тесты
    /// </summary>
    public class SizeTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var size = SizeEntitiesData.SizeEntities.First();
            var sizeTable = SizeTable;

            var id = sizeTable.IdSelect().Compile()(size);

            Assert.Equal(size.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var size = SizeEntitiesData.SizeEntities.First();
            var sizeTable = SizeTable;

            bool isFound = sizeTable.IdPredicate(size.Id).Compile()(size);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var sizes = SizeEntitiesData.SizeEntities;
            var sizeTable = SizeTable;

            bool isFound = sizeTable.IdsPredicate(sizes.Select(category => category.Id)).
                                         Compile()(sizes.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateValueFilter()
        {
            var sizeDomain = SizeData.SizeDomain.First();
            var sizes = SizeEntitiesData.SizeEntities.AsQueryable();
            var sizeTable = SizeTable;
            var sizeEntityConverter = SizeEntityConverterMock.SizeEntityConverter;

            var entities = sizeTable.ValidateFilter(sizes, sizeDomain);
            var domains = sizeEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(1, domains.Value.Count);
            Assert.True(sizeDomain.Equals(domains.Value.First()));
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateCollectionFilter()
        {
            var sizeDomains = SizeData.SizeDomain;
            var sizes = SizeEntitiesData.SizeEntities.AsQueryable();
            var sizeTable = SizeTable;
            var sizeEntityConverter = SizeEntityConverterMock.SizeEntityConverter;


            var entities = sizeTable.ValidateFilter(sizes, sizeDomains);
            var domains = sizeEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(sizeDomains.Count, domains.Value.Count);
            Assert.True(sizeDomains.SequenceEqual(domains.Value));
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<SizeEntity>> DbSet =>
            new Mock<DbSet<SizeEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static ISizeTable SizeTable =>
            new SizeTable(DbSet.Object);
    }
}