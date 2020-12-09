using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Базовая доменная модель
    /// </summary>
    public interface IClothesTypeShortDomain : IClothesTypeShortBase, IDomainModel<string>
    { }
}