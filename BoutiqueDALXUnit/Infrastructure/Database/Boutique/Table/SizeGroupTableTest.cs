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