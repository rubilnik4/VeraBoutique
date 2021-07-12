using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesSortingViewModelItems;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Prism.Common;
using Prism.Navigation;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public class ClothesViewModel : NavigationBaseViewModel<ClothesNavigationParameters>
    {
        public ClothesViewModel(IClothesRestService clothesRestService, IClothesDetailNavigationService clothesDetailNavigationService)
        {
            _clothes = this.WhenAnyValue(x => x.NavigationParameters).
                            Where(clothesParameters => clothesParameters!= null).
                            SelectMany(parameters => Observable.FromAsync(() => GetClothes(parameters!, clothesRestService, clothesDetailNavigationService))).
                            ToProperty(this, nameof(Clothes), scheduler: RxApp.MainThreadScheduler);

            ClothesFilterCommand = ReactiveCommand.Create<Unit, IReadOnlyList<ClothesViewModelItem>>(
                                   _ => FilterViewModelFactory.GetClothesFiltered(Clothes, FilterViewModel.ClothesFilters));
            _sortingViewModel = Observable.Return(new SortingViewModel()).
                                ToProperty(this, nameof(SortingViewModel));

            _filterViewModel = this.WhenAnyValue(x => x.Clothes, x => x.NavigationParameters, (clothes, parameters) => (clothes, parameters)).
                                    Where(clothesNavigation => clothesNavigation.clothes != null && clothesNavigation.parameters != null).
                                    Select(clothesNavigation => new FilterViewModel(clothesNavigation.parameters!, clothesNavigation.clothes, 
                                                                                    ClothesFilterCommand)).
                                    ToProperty(this, nameof(FilterViewModel));

            _clothesFiltered = this.WhenAnyValue(x => x.FilterViewModel).
                                    Where(filterViewModel => filterViewModel != null).
                                    SelectMany(filterViewModel => ClothesFilterCommand).
                                    ToProperty(this, nameof(ClothesFiltered));

            _clothesViewModelColumnItems = this.WhenAnyValue(x => x.ClothesFiltered).
                                                Where(clothes => clothes != null).
                                                Select(GetClothesItems).
                                                ToProperty(this, nameof(ClothesViewModelColumnItems));

            ImagesCommand = ReactiveCommand.CreateFromObservable<int, ImageSource>(
                itemIndex => ClothesViewModelColumnItems[itemIndex].ClothesViewModelItemLeft!.
                             ImageCommand.Execute(Unit.Default));

            this.WhenAnyValue(x => x.Clothes, x => x.FilterViewModel, (clothes, filterViewModel) => (clothes, filterViewModel)).
              Where(clothes => clothes.clothes != null && clothes.filterViewModel != null).
              Select(_ => Unit.Default).
              InvokeCommand(ClothesFilterCommand);
        }

        /// <summary>
        /// Одежда
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyCollection<ClothesViewModelItem>> _clothes;

        /// <summary>
        /// Одежда
        /// </summary>
        private IReadOnlyCollection<ClothesViewModelItem> Clothes =>
            _clothes.Value;

        /// <summary>
        /// Одежда с фильтрами
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyList<ClothesViewModelItem>> _clothesFiltered;

        /// <summary>
        /// Одежда с фильтрами
        /// </summary>
        private IReadOnlyList<ClothesViewModelItem> ClothesFiltered =>
            _clothesFiltered.Value;

        /// <summary>
        /// Одежда. Модели
        /// </summary>
        private readonly ObservableAsPropertyHelper<IReadOnlyList<ClothesColumnViewModelItem>> _clothesViewModelColumnItems;

        /// <summary>
        /// Одежда. Модели
        /// </summary>
        public IReadOnlyList<ClothesColumnViewModelItem> ClothesViewModelColumnItems =>
            _clothesViewModelColumnItems.Value;

        /// <summary>
        /// Сортировка
        /// </summary>
        private readonly ObservableAsPropertyHelper<SortingViewModel> _sortingViewModel;

        /// <summary>
        /// Сортировка
        /// </summary>
        public SortingViewModel SortingViewModel =>
            _sortingViewModel.Value;

        /// <summary>
        /// Фильтрация
        /// </summary>
        private readonly ObservableAsPropertyHelper<FilterViewModel> _filterViewModel;

        /// <summary>
        /// Фильтрация
        /// </summary>
        public FilterViewModel FilterViewModel =>
            _filterViewModel.Value;

        /// <summary>
        /// Команда обновления списка одежды с фильтрами
        /// </summary>
        public ReactiveCommand<Unit, IReadOnlyList<ClothesViewModelItem>> ClothesFilterCommand { get; }

        /// <summary>
        /// Команда загрузки изображений
        /// </summary>
        public ReactiveCommand<int, ImageSource> ImagesCommand { get; }

        /// <summary> 
        /// Получить модели одежды
        /// </summary>
        private static async Task<IReadOnlyCollection<ClothesViewModelItem>> GetClothes(ClothesNavigationParameters clothesParameters,
                                                                                        IClothesRestService clothesRestService,
                                                                                        IClothesDetailNavigationService clothesDetailNavigationService) =>
            await clothesRestService.GetClothesDetails(clothesParameters.GenderType, clothesParameters.ClothesTypeDomain.Name).
            ResultCollectionOkTaskAsync(clothes => clothes.Select(clotheItem =>
                                                            new ClothesViewModelItem(clotheItem, clothesRestService, clothesDetailNavigationService)) ).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   result => result.Value,
                                   result => new List<ClothesViewModelItem>());

        /// <summary>
        /// Преобразовать в модели одежды
        /// </summary>
        private static IReadOnlyList<ClothesColumnViewModelItem> GetClothesItems(IReadOnlyList<ClothesViewModelItem> clothesViewModels) =>
            clothesViewModels.
            Map(clothesItems => (columnLeft: clothesItems.Where((clothes, index) => index % 2 == 0),
                                 columnRight: clothesItems.Where((clothes, index) => index % 2 != 0))).
            Map(clothesPair => clothesPair.columnLeft.ZipLong(clothesPair.columnRight,
                                                              (first, second) => new ClothesColumnViewModelItem(first, second))).
            ToList();
    }
}