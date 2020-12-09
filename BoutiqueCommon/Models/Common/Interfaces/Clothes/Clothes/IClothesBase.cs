using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes
{
    public interface IClothesBase<out TGender, out TClothesType, out TColor, out TSizeGroup, out TSize> : IClothesShortBase
        where TGender : IGenderBase
        where TClothesType : IClothesTypeShortBase
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupBase<TSize>
        where TSize: ISizeBase
    {
        /// <summary>
        /// Тип пола
        /// </summary>
        TGender Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        TClothesType ClothesTypeShort { get; }

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