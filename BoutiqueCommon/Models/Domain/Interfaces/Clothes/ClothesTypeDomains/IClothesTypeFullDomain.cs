using System.Collections.Generic;
using Functional.Models.Interfaces.Result;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Полная информация. Доменная модель
    /// </summary>
    public interface IClothesTypeFullDomain : IClothesTypeDomain
    {
        /// <summary>
        /// Типы пола. Доменная модель
        /// </summary>
        IReadOnlyCollection<IGenderDomain> Genders { get; }

        /// <summary>
        /// Преобразовать в базовую модель
        /// </summary>
        IResultValue<IClothesTypeShortDomain> ToClothesTypeShort();
    }
}