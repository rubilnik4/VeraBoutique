using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes
{
    /// <summary>
    /// Вид одежды. Основная модель
    /// </summary>
    public interface IClothesTypeMainBase<TCategory>: IClothesTypeBase, IEquatable<IClothesTypeMainBase<TCategory>>
        where TCategory: ICategoryBase
    {
        /// <summary>
        /// Категория одежды
        /// </summary>
        TCategory Category { get; }
    }
}