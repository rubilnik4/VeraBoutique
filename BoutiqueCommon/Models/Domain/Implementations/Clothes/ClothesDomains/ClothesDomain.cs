using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    public class ClothesDomain : ClothesMain, IClothesDomain
    {
        public ClothesDomain(IClothesShort clothesShort, string description,
                             IGenderDomain gender, IClothesTypeShortDomain clothesTypeShort,
                             IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
           : this(clothesShort.Id, clothesShort.Name,
                  clothesShort.Price, clothesShort.Image,
                  description, gender, clothesTypeShort, colors, sizeGroups)
        { }

        public ClothesDomain(int id, string name, decimal price, byte[]? image, string description,
                             IGenderDomain gender, IClothesTypeShortDomain clothesTypeShort,
                             IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
            : base(id, name, description, price, image)
        {
            ClothesTypeShort = clothesTypeShort;
            Gender = gender;
            Colors = colors.ToList();
            SizeGroups = sizeGroups.ToList();
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public IGenderDomain Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public IClothesTypeShortDomain ClothesTypeShort { get; }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<IColorClothesDomain> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<ISizeGroupDomain> SizeGroups { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesDomain clothes && Equals(clothes);

        public bool Equals(IClothesDomain? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price &&
            other?.Description == Description &&
            other?.Gender.Equals(Gender) == true &&
            other?.ClothesTypeShort.Equals(ClothesTypeShort) == true &&
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Price, Description,
                             Gender.GetHashCode(), ClothesTypeShort.GetHashCode(),
                             Colors.Average(color => color.GetHashCode()),
                             SizeGroups.Average(size => size.GetHashCode()));
        #endregion
    }
}