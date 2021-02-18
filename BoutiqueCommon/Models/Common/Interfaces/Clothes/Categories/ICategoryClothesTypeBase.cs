using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories
{
    /// <summary>
    /// Категория одежды с подтипами
    /// </summary>
    public interface ICategoryClothesTypeBase<TClothesType>: ICategoryBase, IEquatable<ICategoryClothesTypeBase<TClothesType>>
        where TClothesType: IClothesTypeBase
    {
        /// <summary>
        /// Типы одежды
        /// </summary>
        IReadOnlyCollection<TClothesType>  ClothesTypes { get; }
    }
}