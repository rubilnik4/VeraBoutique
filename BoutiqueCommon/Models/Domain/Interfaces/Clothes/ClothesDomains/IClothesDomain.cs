using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Трансферная модель
    /// </summary>
    public interface IClothesDomain : IClothesShortDomain, IClothesMain
    {
        /// <summary>
        /// Тип пола
        /// </summary>
        IGenderDomain Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        IClothesTypeShortDomain ClothesTypeShort { get; }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        IReadOnlyCollection<IColorClothesDomain> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        IReadOnlyCollection<ISizeGroupDomain> SizeGroups { get; }
    }
}