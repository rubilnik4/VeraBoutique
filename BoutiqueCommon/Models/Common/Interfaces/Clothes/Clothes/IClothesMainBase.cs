﻿using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Полная информация
    /// </summary>
    public interface IClothesMainBase<out TImage, out TGender, out TClothesType, out TColor, out TSizeGroup, TSize> :
        IClothesDetailBase<TColor, TSizeGroup, TSize>
        where TImage : IClothesImageBase
        where TGender : IGenderBase
        where TClothesType : IClothesTypeBase
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupMainBase<TSize>
        where TSize: ISizeBase
    {
        /// <summary>
        /// Изображение
        /// </summary>
        IReadOnlyCollection<TImage> Images { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        TGender Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        TClothesType ClothesType { get; }
    }
}