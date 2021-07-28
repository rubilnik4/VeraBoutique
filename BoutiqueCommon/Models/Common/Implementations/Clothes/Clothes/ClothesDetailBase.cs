using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Уточненная информация
    /// </summary>
    public abstract class ClothesDetailBase<TColor, TSizeGroup, TSize> :
        ClothesBase,
        IClothesDetailBase<TColor, TSizeGroup, TSize>
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupMainBase<TSize>
        where TSize : ISizeBase
    {
        protected ClothesDetailBase(int id, string name, string description, decimal price,
                                    GenderType genderType, string clothesTypeName,
                                    IEnumerable<TColor> colors, IEnumerable<TSizeGroup> sizeGroups)
          : base(id, name, description, price, genderType, clothesTypeName)
        {
            Colors = colors.ToList().AsReadOnly();
            SizeGroups = sizeGroups.ToList().AsReadOnly();
        }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<TColor> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<TSizeGroup> SizeGroups { get; }

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is IClothesDetailBase<TColor, TSizeGroup, TSize> clothes &&
            Equals(clothes);

        public bool Equals(IClothesDetailBase<TColor, TSizeGroup, TSize>? other) =>
            base.Equals(other) &&
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(HashCode.Combine(Id, Name, Price, Description, GenderType, ClothesTypeName),
                             Colors.GetHashCodes(), SizeGroups.GetHashCodes());
        #endregion
    }
}