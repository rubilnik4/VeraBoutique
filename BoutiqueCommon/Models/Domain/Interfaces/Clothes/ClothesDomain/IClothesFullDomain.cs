using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain
{
    /// <summary>
    /// Одежда. Информация. Трансферная модель
    /// </summary>
    public interface IClothesFullDomain : IClothesShortDomain, IClothes
    {
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