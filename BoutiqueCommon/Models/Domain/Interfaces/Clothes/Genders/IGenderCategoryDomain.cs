﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями одежды. Доменная модель
    /// </summary>
    public interface IGenderCategoryDomain: IGenderCategoryBase<ICategoryClothesTypeDomain, IClothesTypeDomain>, IGenderDomain
    { }
}