﻿using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность пола и вида одежды
    /// </summary>
    public class ClothesTypeGenderCompositeEntity: IClothesTypeGenderCompositeEntity
    {
        /// <summary>
        /// Конструктор для базы данных с отсутствующими сущностями
        /// </summary>
        public ClothesTypeGenderCompositeEntity(string clothesType, GenderType genderType)
            : this(clothesType, genderType, null, null)
        {
            ClothesType = clothesType;
            GenderType = genderType;
        }

        public ClothesTypeGenderCompositeEntity(string clothesType, GenderType genderType,
                                        ClothesTypeEntity? clothesTypeEntity, GenderEntity? genderEntity)
        {
            ClothesType = clothesType;
            ClothesTypeEntity = clothesTypeEntity;
            GenderType = genderType;
            GenderEntity = genderEntity;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (string, GenderType) Id => (ClothesType,  GenderType);

        /// <summary>
        /// Идентификатор вида одежды
        /// </summary>
        public string ClothesType { get; }
        
        /// <summary>
        /// Идентификатор пола одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Пол одежды
        /// </summary>
        public GenderEntity? GenderEntity { get; }
    }
}