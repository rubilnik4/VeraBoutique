﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntity
{
    /// <summary>
    /// Одежда. Сущность базы данных
    /// </summary>
    public interface IClothesShortEntity : IClothesShort, IEntityModel<int>
    {
        
    }
}