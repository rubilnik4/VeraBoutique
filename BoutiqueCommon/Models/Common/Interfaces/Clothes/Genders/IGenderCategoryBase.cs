using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями одежды
    /// </summary>
    public interface IGenderCategoryBase<out TCategory, TClothesType> : IGenderBase
        where TCategory: ICategoryClothesTypeBase<TClothesType>
        where TClothesType: IClothesTypeBase
    {
        /// <summary>
        /// Категории одежды
        /// </summary>
        IReadOnlyCollection<TCategory> Categories { get; }
    }
}