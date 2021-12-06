using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities.Carts;
using BoutiqueDALXUnit.Data.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Boutique.Table.Carts
{
    /// <summary>
    /// Таблица базы данных позиций корзины. Тесты
    /// </summary>
    public class CartItemTableTest
    {
        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        [Fact]
        public void IdSelect()
        {
            var cartItem = CartItemEntitiesData.CartItemEntities.First();
            var cartItemTable = CartItemTable;

            var id = cartItemTable.IdSelect().Compile()(cartItem);

            Assert.Equal(cartItem.Id, id);
        }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        [Fact]
        public void IdPredicate()
        {
            var cartItem = CartItemEntitiesData.CartItemEntities.First();
            var cartItemTable = CartItemTable;

            bool isFound = cartItemTable.IdPredicate(cartItem.Id).Compile()(cartItem);

            Assert.True(isFound);
        }

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        [Fact]
        public void IdsPredicate()
        {
            var cartItems = CartItemEntitiesData.CartItemEntities;
            var cartItemTable = CartItemTable;

            bool isFound = cartItemTable.IdsPredicate(cartItems.Select(category => category.Id)).
                                         Compile()(cartItems.First());

            Assert.True(isFound);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<CartItemEntity>> DbSet =>
            new();

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static ICartItemTable CartItemTable =>
            new CartItemTable(DbSet.Object);
    }
}