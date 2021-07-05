using System;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Фильтрация цены
    /// </summary>
    public class FilterPriceViewModelItem
    {
        public FilterPriceViewModelItem(decimal priceMinimum, decimal priceMaximum)
        {
            _priceMinimum = priceMinimum;
            _priceMaximum = priceMaximum;
        }

        /// <summary>
        /// Минимальная цена
        /// </summary>
        private readonly decimal _priceMinimum;

        /// <summary>
        /// Максимальная цена
        /// </summary>
        private readonly decimal _priceMaximum;

        /// <summary>
        /// Минимальная цена
        /// </summary>
        public decimal PriceMinimum =>
           Math.Floor(_priceMinimum);

        /// <summary>
        /// Максимальная цена
        /// </summary>
        public decimal PriceMaximum =>
            Math.Ceiling(_priceMaximum);

        /// <summary>
        /// Шаг цены
        /// </summary>
        public int PriceStep =>
            Math.Log10((double)_priceMaximum).
            Map(Math.Ceiling).
            Map(pow => Math.Pow(10, pow) / 100).
            Map(step => (int)Math.Floor(step));

        /// <summary>
        /// Минимальная цена
        /// </summary>
        public double PriceMinimumValue { get; set; }

        /// <summary>
        /// Максимальная цена
        /// </summary>
        public double PriceMaximumValue { get; set; }
    }
}