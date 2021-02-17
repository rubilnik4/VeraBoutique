﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public abstract class ClothesFullBase<TGender, TClothesType, TColor, TSizeGroup, TSize> :
        ClothesBase,
        IClothesFullBase<TGender, TClothesType, TColor, TSizeGroup, TSize>
        where TGender : IGenderBase
        where TClothesType : IClothesTypeBase
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupFullBase<TSize>
        where TSize : ISizeBase
    {
        protected ClothesFullBase(int id, string name, string description, decimal price, byte[] image,
                              TGender gender, TClothesType clothesType,
                              IEnumerable<TColor> colors, IEnumerable<TSizeGroup> sizeGroups)
            : base(id, name, description, price, image, gender.GenderType, clothesType.Name)
        {
            Gender = gender;
            ClothesType = clothesType;
            Colors = colors.ToList();
            SizeGroups = sizeGroups.ToList();
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public TGender Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public TClothesType ClothesType { get; }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<TColor> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<TSizeGroup> SizeGroups { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesFullBase<TGender, TClothesType, TColor, TSizeGroup, TSize> clothes &&
                                                    Equals(clothes);

        public bool Equals(IClothesFullBase<TGender, TClothesType, TColor, TSizeGroup, TSize>? other) =>
            base.Equals(other) &&
            other?.Gender.Equals(Gender) == true &&
            other?.ClothesTypeShort.Equals(ClothesType) == true &&
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(HashCode.Combine(Id, Name, Price, Description, Image),
                             Gender.GetHashCode(), ClothesType.GetHashCode(),
                             ColorBase.GetColorClothesHashCodes(Colors),
                             SizeGroupBase.GetSizeGroupHashCodes(SizeGroups));
        #endregion
    }
}