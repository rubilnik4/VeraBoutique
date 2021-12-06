using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Carts
{
    /// <summary>
    /// Корзина. Сущность базы данных
    /// </summary>
    public interface ICartEntity: ICartBase, IEntityModel<Guid>
    {
        /// <summary>
        /// Изображение
        /// </summary>
        IReadOnlyCollection<CartItemEntity>? CartItems { get; }
    }
}