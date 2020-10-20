﻿using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public interface IClothesInformationEntity : IClothesInformation, IEntityModel<int>
    { }
}