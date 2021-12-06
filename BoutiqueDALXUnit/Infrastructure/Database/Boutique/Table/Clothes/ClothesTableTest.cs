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
    /// Таблица базы данных одежды. Тесты
    /// </summary>
    public class ClothesTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect_Ok()
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
        public void IdPredicate_Ok()
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
        public void IdsPredicate_Ok()
        {
            var clothes = ClothesEntitiesData.ClothesEntities;
            var clothesTable = ClothesTable;

            bool isFound = clothesTable.IdsPredicate(clothes.Select(category => category.Id)).
                                            Compile()(clothes.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<ClothesEntity>> DbSet =>
            new ();

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        private static IClothesTable ClothesTable =>
            new ClothesTable(DbSet.Object);
    }
}