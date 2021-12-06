using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDALXUnit.Data.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table.Carts
{
    /// <summary>
    /// Таблица базы данных корзины. Тесты
    /// </summary>
    public class CartTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var cart = CartEntitiesData.CartEntities.First();
            var cartTable = CartTable;

            var id = cartTable.IdSelect().Compile()(cart);

            Assert.Equal(cart.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var cart = CartEntitiesData.CartEntities.First();
            var cartTable = CartTable;

            bool isFound = cartTable.IdPredicate(cart.Id).Compile()(cart);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var carts = CartEntitiesData.CartEntities;
            var cartItemTable = CartTable;

            bool isFound = cartItemTable.IdsPredicate(carts.Select(category => category.Id)).
                                         Compile()(carts.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<CartEntity>> DbSet =>
            new();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static ICartTable CartTable =>
            new CartTable(DbSet.Object);
    }
}