using System;
using System.Collections.Generic;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями
    /// </summary>
    public interface IGenderCategorizedBase<TCategory> : IGenderBase, IEquatable<IGenderCategorizedBase<TCategory>>
        where TCategory: ICategoryBase
    {
        /// <summary>
        /// Категории одежды
        /// </summary>
        IReadOnlyCollection<TCategory> Categories { get; }
    }
}