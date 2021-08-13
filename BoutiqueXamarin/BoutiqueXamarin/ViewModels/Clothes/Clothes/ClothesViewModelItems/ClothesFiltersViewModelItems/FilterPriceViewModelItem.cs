using System;
using System.Collections.Generic;
using System.Reactive;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Calculate;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems
{
    /// <summary>
    /// Фильтрация цены
    /// </summary>
    public class FilterPriceViewModelItem
    {
        public FilterPriceViewModelItem(decimal priceMinimum, decimal priceMaximum,
                                        ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand)
        {
            _priceMinimum = priceMinimum;
            _priceMaximum = priceMaximum;
            PriceMinimumValue = (double)_priceMinimum;
            PriceMaximumValue = (double)_priceMaximum;
            ClothesFilterCommand = clothesFilterCommand;
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
        /// Команда обновления фильтрации одежды
        /// </summary>
        public ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> ClothesFilterCommand { get; }

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
            ClothesPrices.GetPriceStep(_priceMaximum);

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