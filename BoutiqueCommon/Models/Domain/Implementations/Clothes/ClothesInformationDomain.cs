using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    public class ClothesInformationDomain : ClothesInformation, IClothesInformationDomain,
                                           IEquatable<IClothesInformationDomain>
    {
        public ClothesInformationDomain(IClothesShort clothesShort,
                                        string description, IClothesTypeDomain clothesType,
                                        IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
           : this(clothesShort.Id, clothesShort.Name,
                  clothesShort.Price, clothesShort.Image,
                  description, clothesType, colors, sizeGroups)
        { }

        public ClothesInformationDomain(int id, string name, decimal price, byte[]? image,
                                        string description, IClothesTypeDomain clothesType,
                                        IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
            : base(id, name, description, price, image)
        {
            ClothesType = clothesType;
            Colors = colors.ToList();
            SizeGroups = sizeGroups.ToList();
        }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public IClothesTypeDomain ClothesType { get; }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<IColorClothesDomain> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<ISizeGroupDomain> SizeGroups { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesInformationDomain clothes && Equals(clothes);

        public bool Equals(IClothesInformationDomain? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price &&
            other?.Description == Description &&
            other?.ClothesType.Equals(ClothesType) == true &&
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Price, Description, ClothesType.GetHashCode(),
                             Colors.Average(color => color.GetHashCode()),
                             SizeGroups.Average(size => size.GetHashCode()));
        #endregion
    }
}