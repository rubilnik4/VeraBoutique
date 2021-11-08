using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.Views.Clothes.Clothes.ClothesViewItems.ClothesFilters;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems
{
    /// <summary>
    /// Модель фильтрации
    /// </summary>
    public class FilterViewModel : BaseViewModel
    {
        public FilterViewModel(ClothesNavigationOptions navigateOptions, IReadOnlyCollection<ClothesViewModelItem> clothes,
                               ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand)
        {
            FilterSizeViewModelItems = FilterAndSortingViewModelFactory.GetFilterSizes(clothes, navigateOptions.SizeTypeDefault,
                                                                                       clothesFilterCommand);
            FilterColorViewModelItems = FilterAndSortingViewModelFactory.GetFilterColors(clothes, clothesFilterCommand);
            FilterPriceViewModelItem = FilterAndSortingViewModelFactory.GetFilterPrices(clothes, clothesFilterCommand);
        }

        /// <summary>
        /// Размеры для фильтрации
        /// </summary>
        public IReadOnlyCollection<FilterSizeViewModelItem> FilterSizeViewModelItems { get; }

        /// <summary>
        /// Размеры для фильтрации
        /// </summary>
        public IReadOnlyCollection<FilterColorViewModelItem> FilterColorViewModelItems { get; }

        /// <summary>
        /// Цены для фильтрации
        /// </summary>
        public FilterPriceViewModelItem FilterPriceViewModelItem { get; }

        /// <summary>
        /// Получить фильтры
        /// </summary>
        public ClothesFilterViewModelCollection ClothesFilters =>
             new ClothesFilterViewModelCollection(FilterSizeViewModelItems, FilterColorViewModelItems, FilterPriceViewModelItem);
    }
}