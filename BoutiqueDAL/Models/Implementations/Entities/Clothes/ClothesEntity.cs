﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesEntity : ClothesBase, IClothesEntity
    {
        public ClothesEntity(int id, string name, string description, decimal price, GenderType genderType, string clothesTypeName)
            : this(id, name, description, price, genderType, clothesTypeName, null, null, null, null, null)
        { }

        public ClothesEntity(IClothesBase clothes)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.GenderType, clothes.ClothesTypeName,
                   null, null, null, null, null)
        { }

        public ClothesEntity(IClothesBase clothes, IEnumerable<ClothesImageEntity> clothesImages,
                             IEnumerable<ClothesColorCompositeEntity> clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupComposites)
           : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.GenderType, clothes.ClothesTypeName,
                  clothesImages, null, null, clothesColorComposites, clothesSizeGroupComposites)
        { }

        public ClothesEntity(IClothesBase clothes, IEnumerable<ClothesImageEntity> clothesImages,
                             GenderEntity gender, ClothesTypeEntity clothesType,
                             IEnumerable<ClothesColorCompositeEntity> clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupComposites)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, gender.GenderType, clothesType.Name,
                   clothesImages, gender, clothesType, clothesColorComposites, clothesSizeGroupComposites)
        { }

        public ClothesEntity(int id, string name, string description, decimal price, GenderType genderType, string clothesTypeName,
                             IEnumerable<ClothesImageEntity>? clothesImages, GenderEntity? gender, ClothesTypeEntity? clothesType,
                             IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites)
          : base(id, name, description, price, genderType, clothesTypeName)
        {
            ClothesImages = clothesImages?.ToList();
            Gender = gender;
            ClothesType = clothesType;
            ClothesColorComposites = clothesColorComposites?.ToList();
            ClothesSizeGroupComposites = clothesSizeGroupComposites?.ToList();
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public IReadOnlyCollection<ClothesImageEntity>? ClothesImages { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public GenderEntity? Gender { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public ClothesTypeEntity? ClothesType { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity>? ClothesColorComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}