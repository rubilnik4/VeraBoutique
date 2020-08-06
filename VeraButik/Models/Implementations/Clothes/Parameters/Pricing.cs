using System;

namespace VeraButik.Models.Implementations.Clothes.Parameters
{
    /// <summary>
    /// Ценообразование
    /// </summary>
    public class Pricing
    {
        public Pricing(decimal price, int amount, string unit)
        {
            if  (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (String.IsNullOrWhiteSpace(unit)) throw new ArgumentNullException(nameof(unit));

            Price = price;
            Amount = amount;
            Unit = unit;
        }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Amount { get; }

        /// <summary>
        /// Единицы измерения
        /// </summary>
        public string Unit { get; }
    }
}