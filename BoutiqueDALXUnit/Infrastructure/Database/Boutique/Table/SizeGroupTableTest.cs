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
    /// Таблица базы данных группы размеров одежды
    /// </summary>
    public class SizeGroupTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var sizeGroup = SizeGroupEntitiesData.SizeGroupEntities.First();
            var sizeGroupTable = SizeGroupTable;

            var id = sizeGroupTable.IdSelect().Compile()(sizeGroup);

            Assert.Equal(sizeGroup.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var sizeGroup = SizeGroupEntitiesData.SizeGroupEntities.First();
            var sizeGroupTable = SizeGroupTable;

            bool isFound = sizeGroupTable.IdPredicate(sizeGroup.Id).Compile()(sizeGroup);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var sizeGroups = SizeGroupEntitiesData.SizeGroupEntities;
            var sizeGroupTable = SizeGroupTable;

            bool isFound = sizeGroupTable.IdsPredicate(sizeGroups.Select(category => category.Id)).
                                          Compile()(sizeGroups.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateValueFilter()
        {
            var sizeGroupDomain = SizeGroupData.SizeGroupDomain.First();
            var sizeGroups = SizeGroupEntitiesData.SizeGroupEntities.AsQueryable();
            var sizeGroupTable = SizeGroupTable;
            var sizeGroupEntityConverter = SizeGroupEntityConverterMock.SizeGroupEntityConverter;

            var entities = sizeGroupTable.ValidateFilter(sizeGroups, sizeGroupDomain);
            var domains = sizeGroupEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(1, domains.Value.Count);
            Assert.True(sizeGroupDomain.Equals(domains.Value.First()));
        }

        /// <summary>
        /// Функция выбора сущностей для проверки наличия
        /// </summary>
        [Fact]
        public void ValidateCollectionFilter()
        {
            var sizeGroupDomains = SizeGroupData.SizeGroupDomain;
            var sizeGroups = SizeGroupEntitiesData.SizeGroupEntities.AsQueryable();
            var sizeGroupTable = SizeGroupTable;
            var sizeGroupEntityConverter = SizeGroupEntityConverterMock.SizeGroupEntityConverter;

            var entities = sizeGroupTable.ValidateFilter(sizeGroups, sizeGroupDomains);
            var domains = sizeGroupEntityConverter.FromEntities(entities);

            Assert.True(domains.OkStatus);
            Assert.Equal(sizeGroupDomains.Count, domains.Value.Count);
            Assert.True(sizeGroupDomains.SequenceEqual(domains.Value));
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<SizeGroupEntity>> DbSet =>
            new Mock<DbSet<SizeGroupEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static ISizeGroupTable SizeGroupTable =>
            new SizeGroupTable(DbSet.Object);
    }
}