using System;
using VeraButik.Models.Implementations.Clothes.Size;

namespace VeraButik.Models.Implementations.Clothes.Parameters
{
    /// <summary>
    /// Параметры одежды
    /// </summary>
    public class AttributesClothes
    {
        public AttributesClothes(NumericSize size, string color)
        {
            if (String.IsNullOrWhiteSpace(color)) throw new ArgumentNullException(nameof(color));
          
            Size = size;
            Color = color;
        }

        /// <summary>
        /// Размер
        /// </summary>
        public NumericSize Size { get; }

        /// <summary>
        /// Цвет
        /// </summary>
        public string Color { get; }
    }
    }
}