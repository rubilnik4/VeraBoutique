﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities
{
    /// <summary>
    /// Одежда. Базовая сущность базы данных
    /// </summary>
    public interface IClothesShortEntity : IClothesMain, IEntityModel<int>
    { }
}