using System;

namespace VeraButik.Models.Implementations.Clothes.Size
{
    /// <summary>
    /// Размер одежды в числовом виде
    /// </summary>
    public class NumericSize
    {
        public NumericSize(int minimum, int maximum)
        {
            if (minimum <= 0) throw new ArgumentOutOfRangeException(nameof(minimum));
            if (maximum <= 0) throw new ArgumentOutOfRangeException(nameof(maximum));

            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Минимальный диапазон
        /// </summary>
        public int Minimum { get; }

        /// <summary>
        /// Максимальный диапазон
        /// </summary>
        public int Maximum { get; }
    }
}