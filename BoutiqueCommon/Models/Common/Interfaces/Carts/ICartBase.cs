﻿using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public interface ICartBase: IModel<Guid>, IEquatable<ICartBase>
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// Идентификатор создателя
        /// </summary>
        string AuthorId { get; }
    }
}