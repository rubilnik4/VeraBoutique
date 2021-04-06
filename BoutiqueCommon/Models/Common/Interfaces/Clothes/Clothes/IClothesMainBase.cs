using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    public interface IClothesMainBase<TGender, TClothesType, TColor, TSizeGroup, TSize> :
        IClothesBase,
        IEquatable<IClothesMainBase<TGender, TClothesType, TColor, TSizeGroup, TSize>>
        where TGender : IGenderBase
        where TClothesType : IClothesTypeBase
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupMainBase<TSize>
        where TSize: ISizeBase
    {
        /// <summary>
        /// Изображение
        /// </summary>
        byte[] Image { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        TGender Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        TClothesType ClothesType { get; }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        IReadOnlyCollection<TColor> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<TSizeGroup> SizeGroups { get; }
    }
}