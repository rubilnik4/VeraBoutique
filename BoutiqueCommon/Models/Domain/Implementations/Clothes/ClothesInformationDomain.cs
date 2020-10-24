using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    public class ClothesInformationDomain : ClothesInformation, IClothesInformationDomain,
                                           IEquatable<IClothesInformationDomain>
    {
        public ClothesInformationDomain(IClothesShort clothesShort,
                                        string description, IGenderDomain gender, IClothesTypeDomain clothesType,
                                        IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizes)
           : this(clothesShort.Id, clothesShort.Name,
                  clothesShort.Price, clothesShort.Image,
                  description, gender, clothesType, colors, sizes)
        { }

        public ClothesInformationDomain(int id, string name, decimal price, byte[]? image,
                                        string description, IGenderDomain gender, IClothesTypeDomain clothesType,
                                        IEnumerable<IColorClothesDomain> colors, IEnumerable<ISizeGroupDomain> sizes)
            : base(id, name, description, price, image)
        {
            Gender = gender;
            ClothesType = clothesType;
            Colors = colors.ToList();
            SizeGroups = sizes.ToList();
        }

        /// <summary>
        /// Пол одежды
        /// </summary>
        public IGenderDomain Gender { get; }

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
            other?.Gender.Equals(Gender) == true &&
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.SizeGroups.SequenceEqual(SizeGroups) == true;

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Price, Description, 
                             ClothesType.GetHashCode(), Gender.GetHashCode(),
                             Colors.Average(color => color.GetHashCode()),
                             SizeGroups.Average(size => size.GetHashCode()));
        #endregion
    }
}