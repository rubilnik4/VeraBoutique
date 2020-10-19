using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Одежда. Информация
    /// </summary>
    public abstract class ClothesInformation : ClothesShort, IClothesInformation
    {
        protected ClothesInformation(int id, string name, string description, GenderType genderType,
                                    string clothesType, IReadOnlyCollection<int> sizes, 
                                    decimal price, byte[]? image)
            :base(id, name, price, image)
        {
            Description = description;
            GenderType = genderType;
            ClothesType = clothesType;
            Sizes = sizes;
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Пол
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public string ClothesType { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<int> Sizes { get; }
    }
}