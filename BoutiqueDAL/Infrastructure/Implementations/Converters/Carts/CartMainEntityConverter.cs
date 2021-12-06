using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Carts;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Carts
{
    /// <summary>
    /// Преобразования модели корзины в модель базы данных
    /// </summary>
    public class CartMainEntityConverter : EntityConverter<Guid, ICartMainDomain, CartEntity>, ICartMainEntityConverter
    {
        public CartMainEntityConverter(ICartItemEntityConverter cartItemEntityConverter)
        {
            _cartItemEntityConverter = cartItemEntityConverter;
        }

        /// <summary>
        /// Преобразования модели позиции в корзине в модель базы данных
        /// </summary>
        private readonly ICartItemEntityConverter _cartItemEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<ICartMainDomain> FromEntity(CartEntity cartEntity) =>
            GetCartFunc(cartEntity).
            ResultValueCurryOk(GetCartItems(cartEntity.CartItems)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override CartEntity ToEntity(ICartMainDomain cartDomain) =>
            new(cartDomain, _cartItemEntityConverter.ToEntities(cartDomain.CartItems));

        /// <summary>
        /// Функция получения категории одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<ICartItemDomain>, ICartMainDomain>> GetCartFunc(ICartBase cart) =>
            new ResultValue<Func<IEnumerable<ICartItemDomain>, ICartMainDomain>>(
                cartItems => new CartMainDomain(cart, cartItems));

        /// <summary>
        /// Получить сущности типа пола одежды
        /// </summary>
        private IResultCollection<ICartItemDomain> GetCartItems(IReadOnlyCollection<CartItemEntity>? cartItemEntities) =>
            cartItemEntities.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(cartItemEntities, GetType())).
            ToResultCollection().
            ResultCollectionBindOk(cartItems => _cartItemEntityConverter.FromEntities(cartItems));
    }
}