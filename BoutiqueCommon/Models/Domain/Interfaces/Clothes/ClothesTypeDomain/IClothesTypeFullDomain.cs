using System.Collections.Generic;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain
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
    }
}