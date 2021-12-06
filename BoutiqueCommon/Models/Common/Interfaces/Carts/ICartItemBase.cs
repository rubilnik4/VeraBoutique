using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Carts
{
    /// <summary>
    /// Позиция в корзине
    /// </summary>
    public interface ICartItemBase : IModel<Guid>, IEquatable<ICartItemBase>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Цена
        /// </summary>
        decimal Price { get; }

        /// <summary>
        /// Корзина
        /// </summary>
        Guid CartId { get; }
    }
}