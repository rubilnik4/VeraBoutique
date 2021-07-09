using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems
{
    /// <summary>
    /// Размер для фильтрации
    /// </summary>
    public class FilterSizeViewModelItem : FilterCheckViewModelItem
    {
        public FilterSizeViewModelItem(ISizeGroupMainDomain sizeGroup, SizeType sizeTypeDefault,
                                       ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> clothesFilterCommand)
            : base(clothesFilterCommand)
        {
            SizeGroup = sizeGroup;
            _sizeTypeDefault = sizeTypeDefault;
        }

        /// <summary>
        /// Размер одежды
        /// </summary>
        public ISizeGroupMainDomain SizeGroup { get; }

        /// <summary>
        /// Размер по умолчанию
        /// </summary>
        private readonly SizeType _sizeTypeDefault;

        /// <summary>
        /// Наименование размера
        /// </summary>
        public string SizeName =>
            SizeGroup.Sizes.
            First(size => size.SizeType == _sizeTypeDefault).
            SizeNameShort;
    }
}