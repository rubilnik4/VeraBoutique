using System.Collections.Generic;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems
{
    public class ClothesFilterViewModelCollection
    {
        public ClothesFilterViewModelCollection(IReadOnlyCollection<FilterSizeViewModelItem> sizeItems,
                                                IReadOnlyCollection<FilterColorViewModelItem> colorItems,
                                                FilterPriceViewModelItem priceItem)
        {
            SizeItems = sizeItems;
            ColorItems = colorItems;
            PriceItem = priceItem;
        }

        /// <summary>
        /// Размеры для фильтрации
        /// </summary>
        public IReadOnlyCollection<FilterSizeViewModelItem> SizeItems { get; }

        /// <summary>
        /// Цвета для фильтрации
        /// </summary>
        public IReadOnlyCollection<FilterColorViewModelItem> ColorItems { get; }

        /// <summary>
        /// Фильтрация цены
        /// </summary>
        public FilterPriceViewModelItem PriceItem { get; }

    }
}