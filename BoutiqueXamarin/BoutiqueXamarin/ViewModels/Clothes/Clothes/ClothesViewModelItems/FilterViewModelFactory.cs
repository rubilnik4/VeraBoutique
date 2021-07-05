using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Модели фильтрации
    /// </summary>
    public static class FilterViewModelFactory
    {
        /// <summary>
        /// Получить размеры для фильтрации
        /// </summary>
        public static IReadOnlyCollection<FilterSizeViewModelItem> GetFilterSizes(IEnumerable<IClothesDetailDomain> clothes,
                                                                                   SizeType sizeTypeDefault) =>
            clothes.
            SelectMany(clothesItem => clothesItem.SizeGroups).
            Distinct().
            OrderBy(sizeGroup => sizeGroup.SizeNormalize).
            Select(sizeGroup => sizeGroup.Sizes.FirstOrDefault(size => size.SizeType == sizeTypeDefault)).
            Where(size => size != null).
            Distinct().
            Select(size => new FilterSizeViewModelItem(size!)).
            ToList();

        /// <summary>
        /// Получить цвета для фильтрации
        /// </summary>
        public static IReadOnlyCollection<FilterColorViewModelItem> GetFilterColors(IEnumerable<IClothesDetailDomain> clothes) =>
            clothes.
            SelectMany(clothesItem => clothesItem.Colors).
            Distinct().
            OrderBy(color => color.Name).
            Select(color => new FilterColorViewModelItem(color)).
            ToList();

        /// <summary>
        /// Получить цены для фильтрации
        /// </summary>
        public static FilterPriceViewModelItem GetFilterPrices(IEnumerable<IClothesDetailDomain> clothes) =>
            clothes.
            Select(clothesItem => clothesItem.Price).
            OrderBy(price => price).
            ToList().
            Map(prices => new FilterPriceViewModelItem(prices.First(), prices.Last()));
    }
}