﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей одежды
    /// </summary>
    public class ClothesShortEntitiesData
    {
        /// <summary>
        /// Сущности одежды
        /// </summary>
        public static IList<ClothesShortEntity> ClothesShortEntities =>
            ClothesData.ClothesShortDomains.
            Select(clothesShort => new ClothesShortEntity(clothesShort.Id, clothesShort.Name,
                                                          clothesShort.Price, clothesShort.Image)).
            ToList();
    }
}