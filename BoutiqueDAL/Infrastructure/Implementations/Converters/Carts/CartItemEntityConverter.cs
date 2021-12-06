using System;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Carts
{
    /// <summary>
    /// Преобразования модели позиции в корзине в модель базы данных
    /// </summary>
    public class CartItemEntityConverter : EntityConverter<Guid, ICartItemDomain, CartItemEntity>, ICartItemEntityConverter
    {
        /// <summary>
        /// Преобразовать позицию в корзине из модели базы данных
        /// </summary>
        public override IResultValue<ICartItemDomain> FromEntity(CartItemEntity cartItemEntity) =>
            new CartItemDomain(cartItemEntity).
            ToResultValue();

        /// <summary>
        /// Преобразовать позицию в корзине в модель базы данных
        /// </summary>
        public override CartItemEntity ToEntity(ICartItemDomain cartItemDomain) =>
            new(cartItemDomain);
    }
}