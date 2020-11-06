using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public interface IClothesTypeDomain : IClothesTypeShortDomain
    {
        /// <summary>
        /// Типы пола. Доменная модель
        /// </summary>
        IReadOnlyCollection<IGenderDomain> Genders { get; }
    }
}