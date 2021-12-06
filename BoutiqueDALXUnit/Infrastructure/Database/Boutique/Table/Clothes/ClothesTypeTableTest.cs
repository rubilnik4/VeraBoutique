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