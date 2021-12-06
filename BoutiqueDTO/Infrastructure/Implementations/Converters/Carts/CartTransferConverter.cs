using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDTO.Models.Implementations.Carts;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Carts
{
    /// <summary>
    /// Конвертер корзины в трансферную модель
    /// </summary>
    public class CartTransferConverter : TransferConverter<Guid, ICartDomain, CartTransfer>, ICartTransferConverter
    {
        /// <summary>
        /// Преобразовать тип одежды в трансферную модель
        /// </summary>
        public override CartTransfer ToTransfer(ICartDomain cartDomain) =>
            new CartTransfer(cartDomain);

        /// <summary>
        /// Преобразовать тип одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICartDomain> FromTransfer(CartTransfer cartTransfer) =>
            new CartDomain(cartTransfer).ToResultValue();
    }
}