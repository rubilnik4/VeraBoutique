using System.Collections.Generic;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using BoutiqueXamarin.Models.Enums;
using BoutiqueXamarin.ViewModels.Base;
using ReactiveUI;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesSortingViewModelItems
{
    /// <summary>
    /// Сортировка
    /// </summary>
    public class SortingViewModel : BaseViewModel
    {
        public SortingViewModel(ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> filterCommand)
        {
            _clothesSortingType = this.WhenAnyValue(x => x.IsNamingSorting, x => x.IsPriceSorting,
                                                    (isNaming, isPrice) => (isNaming, isPrice)).
                                       Where(sorting => SortingTypeValidate(sorting.isNaming, sorting.isPrice)).
                                       Select(sorting => ToClothesSortingType(sorting.isNaming, sorting.isPrice)).
                                       ToProperty(this, nameof(ClothesSortingType));
            this.WhenAnyValue(x => x.ClothesSortingType).
                 Where(_ => IsLoaded).
                 Subscribe(_ => filterCommand.Execute(Unit.Default).Subscribe());

            IsLoaded = true;
        }

        

        /// <summary>
        /// Тип сортировки. Наименование
        /// </summary>
        private bool _isNamingSorting = true;

        /// <summary>
        /// Тип сортировки. Наименование
        /// </summary>
        public bool IsNamingSorting
        {
            get => _isNamingSorting;
            set => this.RaiseAndSetIfChanged(ref _isNamingSorting, value);
        }

        /// <summary>
        /// Тип сортировки. Цена
        /// </summary>
        private bool _isPriceSorting;

        /// <summary>
        /// Тип сортировки. Цена
        /// </summary>
        public bool IsPriceSorting
        {
            get => _isPriceSorting;
            set => this.RaiseAndSetIfChanged(ref _isPriceSorting, value);
        }

        /// <summary>
        /// Тип сортировки
        /// </summary>
        private readonly ObservableAsPropertyHelper<ClothesSortingType> _clothesSortingType;

        /// <summary>
        /// Тип сортировки
        /// </summary>
        public ClothesSortingType ClothesSortingType =>
            _clothesSortingType.Value;

        /// <summary>
        /// Проверить тип сортировки
        /// </summary>
        private static bool SortingTypeValidate(bool isNaming, bool isSorting) =>
            (isNaming, isSorting) switch
            {
                (true, false) => true,
                (false, true) => true,
                (_, _) => false,
            };

        /// <summary>
        /// Преобразовать в тип сортировки
        /// </summary>
        private static ClothesSortingType ToClothesSortingType(bool isNaming, bool isSorting) =>
            (isNaming, isSorting) switch
            {
                (true, false) => ClothesSortingType.Naming,
                (false, true) => ClothesSortingType.Price,
                (_, _) => throw new SwitchExpressionException(nameof(ClothesSortingType)),
            };
    }
}