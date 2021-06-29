using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public abstract class ClothesMainBase<TGender, TClothesType, TColor, TSizeGroup, TSize> :
        ClothesDetailBase<TColor, TSizeGroup, TSize>,
        IClothesMainBase<TGender, TClothesType, TColor, TSizeGroup, TSize>
        where TGender : IGenderBase
        where TClothesType : IClothesTypeBase
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupMainBase<TSize>
        where TSize : ISizeBase
    {
        protected ClothesMainBase(int id, string name, string description, decimal price, byte[] image,
                                  TGender gender, TClothesType clothesType,
                                  IEnumerable<TColor> colors, IEnumerable<TSizeGroup> sizeGroups)
            : base(id, name, description, price, gender.GenderType, clothesType.Name, colors, sizeGroups)
        {
            Image = image;
            Gender = gender;
            ClothesType = clothesType;
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        public TGender Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public TClothesType ClothesType { get; }


        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is IClothesMainBase<TGender, TClothesType, TColor, TSizeGroup, TSize> clothes &&
            Equals(clothes);

        public bool Equals(IClothesMainBase<TGender, TClothesType, TColor, TSizeGroup, TSize>? other) =>
            base.Equals(other) &&
            other?.Image.SequenceEqual(Image) == true &&
            other?.Gender.Equals(Gender) == true &&
            other?.ClothesType.Equals(ClothesType) == true &&
            other?.Colors.Cast<IColorBase>().SequenceEqual(Colors.Cast<IColorBase>()) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(HashCode.Combine(Id, Name, Price, Description), 
                             Image, Gender.GetHashCode(), ClothesType.GetHashCode(),
                             Colors.GetHashCodes(), SizeGroups.GetHashCodes());
        #endregion
    }
}