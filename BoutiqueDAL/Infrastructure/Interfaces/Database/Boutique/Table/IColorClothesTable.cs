﻿using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных цвета одежды
    /// </summary>
    public interface IColorClothesTable : IDatabaseTable<string, ColorClothesEntity>
    { }
}