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
    /// Таблица базы данных типа пола. Тесты
    /// </summary>
    public class GenderTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var gender = GenderEntitiesData.GenderEntities.First();
            var genderTable = GenderTable;

            var id = genderTable.IdSelect().Compile()(gender);

            Assert.Equal(gender.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var gender = GenderEntitiesData.GenderEntities.First();
            var genderTable = GenderTable;

            bool isFound = genderTable.IdPredicate(gender.Id).Compile()(gender);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var genders = GenderEntitiesData.GenderEntities;
            var genderTable = GenderTable;

            bool isFound = genderTable.IdsPredicate(genders.Select(gender => gender.Id)).
                                       Compile()(genders.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<GenderEntity>> DbSet =>
            new Mock<DbSet<GenderEntity>>();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static IGenderTable GenderTable =>
            new GenderTable(DbSet.Object);
    }
}