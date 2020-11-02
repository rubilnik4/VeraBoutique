using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomain
{
    public class ClothesFullDomain : ClothesInformation, IClothesFullDomain, IEquatable<IClothesFullDomain>
    {
        public ClothesFullDomain(IClothesShort clothesShort,
                             string description, IClothesTypeShortDomain clothesTypeShort,
                             IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
           : this(clothesShort.Id, clothesShort.Name,
                  clothesShort.Price, clothesShort.Image,
                  description, clothesTypeShort, colors, sizeGroups)
        { }

        public ClothesFullDomain(int id, string name, decimal price, byte[]? image,
                             string description, IClothesTypeShortDomain clothesTypeShort,
                             IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
            : base(id, name, description, price, image)
        {
            ClothesTypeShort = clothesTypeShort;
            Colors = colors.ToList();
            SizeGroups = sizeGroups.ToList();
        }

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
        public override bool Equals(object? obj) => obj is IClothesFullDomain clothes && Equals(clothes);

        public bool Equals(IClothesFullDomain? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price &&
            other?.Description == Description &&
            other?.ClothesTypeShort.Equals(ClothesTypeShort) == true &&
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Price, Description, ClothesTypeShort.GetHashCode(),
                             Colors.Average(color => color.GetHashCode()),
                             SizeGroups.Average(size => size.GetHashCode()));
        #endregion
    }
}