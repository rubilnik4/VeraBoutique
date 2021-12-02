using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Carts;
using BoutiqueDTO.Models.Implementations.Clothes;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Carts
{
    /// <summary>
    /// Конвертер позиций корзины в трансферную модель
    /// </summary>
    public class CartItemTransferConverter : TransferConverter<string, ICartItemDomain, CartItemTransfer>,
                                             ICartItemTransferConverter
    {
        /// <summary>
        /// Преобразовать размеры одежды в трансферную модель
        /// </summary>
        public override CartItemTransfer ToTransfer(ICartItemDomain cartItemDomain) =>
            new CartItemTransfer(cartItemDomain);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICartItemDomain> FromTransfer(CartItemTransfer cartItemTransfer) =>
            new CartItemDomain(cartItemTransfer).
            ToResultValue();
    }
}