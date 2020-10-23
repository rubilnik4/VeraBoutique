﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public interface IClothesTypeDomain : IClothesType, IDomainModel<string>
    {
        /// <summary>
        /// Категория одежды. Доменная модель
        /// </summary>
        ICategoryDomain CategoryDomain { get; }
    }
}