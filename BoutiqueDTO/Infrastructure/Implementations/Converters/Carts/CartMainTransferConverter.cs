using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Carts
{
    /// <summary>
    /// Конвертер корзины в трансферную модель
    /// </summary>
    public class CartMainTransferConverter : TransferConverter<Guid, ICartMainDomain, CartMainTransfer>,
                                             ICartMainTransferConverter
    {
        public CartMainTransferConverter(ICartItemTransferConverter cartItemTransferConverter)
        {
            _cartItemTransferConverter = cartItemTransferConverter;
        }

        /// <summary>
        /// Конвертер позиций корзины в трансферную модель
        /// </summary>
        private readonly ICartItemTransferConverter _cartItemTransferConverter;

        /// <summary>
        /// Преобразовать тип одежды в трансферную модель
        /// </summary>
        public override CartMainTransfer ToTransfer(ICartMainDomain cartMainDomain) =>
            new CartMainTransfer(cartMainDomain,
                                 _cartItemTransferConverter.ToTransfers(cartMainDomain.CartItems));

        /// <summary>
        /// Преобразовать тип одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICartMainDomain> FromTransfer(CartMainTransfer cartMainTransfer) =>
            GetCartFunc(cartMainTransfer).
            ResultValueCurryOk(_cartItemTransferConverter.GetDomains(cartMainTransfer.CartItems)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<ICartItemDomain>, ICartMainDomain>> GetCartFunc(ICartBase cart) =>
            new ResultValue<Func<IEnumerable<ICartItemDomain>, ICartMainDomain>>(
                cartItems => new CartMainDomain(cart, cartItems));
    }
}