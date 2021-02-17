using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Трансферная модель
    /// </summary>
    public interface IClothesFullDomain : 
        IClothesFullBase<IGenderDomain, IClothesTypeDomain, IColorDomain, ISizeGroupFullDomain, ISizeDomain>,
        IClothesDomain
    { }
}