using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public interface ICartBase<out TCartItem> : IModel<string>
        where TCartItem : ICartItemBase
    {
        /// <summary>
        /// Позиции в корзине
        /// </summary>
        IReadOnlyCollection<TCartItem> CartItems { get; }

        /// <summary>
        /// Сумма
        /// </summary>
        decimal Total { get; }
    }
}