using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes
{
    /// <summary>
    /// Вид одежды. Базовые данные
    /// </summary>
    public interface IClothesTypeBase<out TCategory, out TGender>: IClothesTypeShortBase
        where TCategory: ICategoryBase
        where TGender: IGenderBase
    {
        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        TCategory Category { get; }

        /// <summary>
        /// Типы пола. Доменная модель
        /// </summary>
        IReadOnlyCollection<TGender> Genders { get; }
    }
}