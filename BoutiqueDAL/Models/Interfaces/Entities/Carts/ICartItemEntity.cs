using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Carts;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Carts
{
    /// <summary>
    /// Позиция в корзине. Сущность базы данных
    /// </summary>
    public interface ICartItemEntity : ICartItemBase, IEntityModel<Guid>
    {
        /// <summary>
        /// Корзина
        /// </summary>
        CartEntity? CartEntity { get; }
    }
}