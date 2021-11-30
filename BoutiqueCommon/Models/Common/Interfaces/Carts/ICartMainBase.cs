using System.Collections.Generic;

namespace BoutiqueCommon.Models.Common.Interfaces.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public interface ICartMainBase<out TCartItem>: ICartBase
        where TCartItem : ICartItemBase
    {
        /// <summary>
        /// Позиции в корзине
        /// </summary>
        IReadOnlyCollection<TCartItem> CartItems { get; }
    }
}