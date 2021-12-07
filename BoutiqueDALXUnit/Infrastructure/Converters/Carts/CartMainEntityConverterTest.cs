using System.Linq;
using BoutiqueCommonXUnit.Data.Carts;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Entities.Carts;
using BoutiqueDALXUnit.Data.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Carts;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Carts
{
    /// <summary>
    /// Преобразования модели корзины в модель базы данных. Тесты
    /// </summary>
    public class CartMainEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity()
        {
            var cartDomain = CartData.CartMainDomains.First();
            var cartEntityConverter = CartEntityConverterMock.CartMainEntityConverter;

            var cartEntity = cartEntityConverter.ToEntity(cartDomain);

            Assert.True(cartDomain.Equals(cartEntity));
        }

        /// <summary>
        /// Преобразования модели цвета одежды из модели базы данных
        /// </summary>
        [Fact]
        public void FromEntity()
        {
            var carEntity = CartEntitiesData.CartEntities.First();
            var cartEntityConverter = CartEntityConverterMock.CartMainEntityConverter;

            var cartDomain = cartEntityConverter.FromEntity(carEntity);

            Assert.True(cartDomain.OkStatus);
            Assert.True(CartData.CartMainDomains.First().Equals(cartDomain.Value));
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка изображений
        /// </summary>
        [Fact]
        public void FromEntity_ImagesNotFound()
        {
            var carEntity = CartEntitiesData.CartEntities.First();
            var cartNull = new CartEntity(carEntity, null);
            var cartEntityConverter = CartEntityConverterMock.CartMainEntityConverter;

            var cartAfterConverter = cartEntityConverter.FromEntity(cartNull);

            Assert.True(cartAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(cartAfterConverter.Errors.First());
        }
    }
}