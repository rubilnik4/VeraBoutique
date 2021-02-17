using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями одежды
    /// </summary>
    public interface IGenderCategoryBase<TCategory, TClothesType> : IGenderBase, IEquatable<IGenderCategoryBase<TCategory, TClothesType>>
        where TCategory: ICategoryClothesTypeBase<TClothesType>
        where TClothesType: IClothesTypeBase
    {
        /// <summary>
        /// Категории одежды
        /// </summary>
        IReadOnlyCollection<TCategory> Categories { get; }
    }
}