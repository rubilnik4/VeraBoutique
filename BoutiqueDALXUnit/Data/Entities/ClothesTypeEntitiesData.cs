﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные сущностей вида одежды
    /// </summary>
    public static class ClothesTypeEntitiesData
    {
        /// <summary>
        /// Сущности вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeEntities =>
            ClothesTypeData.ClothesTypeMainDomains.
            Select(clothesTypeDomain => new ClothesTypeEntity(clothesTypeDomain, 
                                                              new CategoryEntity(clothesTypeDomain.Category.Name))).
            ToList();

        /// <summary>
        /// Получить сущности типа одежды c информацией об одежде
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> GetClothesTypeEntitiesWithClothes(IEnumerable<ClothesTypeEntity> clothesTypeEntities,
                                                                                               IEnumerable<ClothesEntity> clothesEntities) =>
            clothesTypeEntities.
            Select(clothesType => new ClothesTypeEntity(clothesType.Name, clothesType.SizeTypeDefault, clothesType.CategoryName,
                                                        clothesType.Category, clothesEntities)).
            ToList();

        /// <summary>
        /// Получить сущности типа одежды c типом одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> GetClothesTypeEntitiesWithCategory(IEnumerable<ClothesTypeEntity> clothesTypeEntities,
                                                                                                CategoryEntity categoryEntity) =>
            clothesTypeEntities.
            Select(clothesType => new ClothesTypeEntity(clothesType.Name, clothesType.SizeTypeDefault, categoryEntity)).
            ToList();
    }
}