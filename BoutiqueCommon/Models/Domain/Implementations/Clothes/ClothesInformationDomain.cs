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
    public class ClothesInformationDomain: ClothesInformation, IClothesInformationDomain, 
                                           IEquatable<IClothesInformationDomain>
    {
        public ClothesInformationDomain(int id, string name, string description,
                                        IReadOnlyCollection<string> colors, IReadOnlyCollection<int> sizes,
                                        decimal price, byte[]? image)
            : base(id, name, description, price, image)
        {
            Colors = colors;
            Sizes = sizes;
        }

        /// <summary>
        /// Цвета одежды
        /// </summary>
        public IReadOnlyCollection<string> Colors { get; }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<int> Sizes { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IClothesInformationDomain clothes && Equals(clothes);

        public bool Equals(IClothesInformationDomain? other) =>
            other?.Id == Id && other?.Name == Name && other?.Price == Price &&
            other?.Description == Description && 
            other?.Colors.SequenceEqual(Colors) == true &&
            other?.Sizes.SequenceEqual(Sizes) == true;

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name, Price, Description,
                             Colors.Average(color => color.GetHashCode()),
                             Sizes.Average(size => size.GetHashCode()));
        #endregion
    }
}