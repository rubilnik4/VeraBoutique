using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Базовая доменная модель
    /// </summary>
    public interface IClothesTypeShortDomain : IClothesType, IDomainModel<string>, IEquatable<IClothesTypeShortDomain>
    {
        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        ICategoryDomain Category { get; }
    }
}