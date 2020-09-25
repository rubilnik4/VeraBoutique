﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы, связующей пол и вид одежды
    /// </summary>
    public static class ClothesTypeGenderInitialize
    {
        /// <summary>
        /// Начальные данные, связующие пол и вид одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeGenderEntity> ClothesTypeGenderData =>
            ClothesTypeInitialize.ClothesTypeWithGenderData.
            SelectMany(clothesTypeWithGender => 
                clothesTypeWithGender.Genders.
                Select(gender => new ClothesTypeGenderEntity(clothesTypeWithGender.ClothesType.Id, gender.Id))).
            ToList().AsReadOnly();
    }
}