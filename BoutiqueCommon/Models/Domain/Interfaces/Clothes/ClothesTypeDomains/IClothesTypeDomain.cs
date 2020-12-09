using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public interface IClothesTypeDomain : IClothesTypeBase<ICategoryDomain, IGenderDomain>, IClothesTypeShortDomain
    { }
}