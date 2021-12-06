using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Services.Carts.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDALXUnit.Data.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Carts
{
    /// <summary>
    /// Сервис проверки данных из базы корзины
    /// </summary>
    public class CartDatabaseValidateServiceTest : CartDatabaseValidateService
    {
        public CartDatabaseValidateServiceTest()
           : base(CartTable.Object)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var cart = CartData.CartMainDomains.First();

            var result = ValidateModel(cart);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Таблица базы данных корзины
        /// </summary>
        private static Mock<ICartTable> CartTable =>
            new();
    }
}