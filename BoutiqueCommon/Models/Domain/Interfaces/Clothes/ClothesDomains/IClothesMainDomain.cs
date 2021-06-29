using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public interface IClothesMainDomain: 
        IClothesMainBase<IGenderDomain, IClothesTypeDomain, IColorDomain, ISizeGroupMainDomain, ISizeDomain>,
        IClothesDetailDomain
    { }
}