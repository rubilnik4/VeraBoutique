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
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
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
    public class ClothesViewModel : NavigationBaseViewModel<ClothesNavigationParameters, IClothesNavigationService>
    {
        public ClothesViewModel(IClothesRestService clothesRestService, IClothesNavigationService clothesNavigationService,
                                IChoiceNavigationService choiceNavigationService,
                                IClothesDetailNavigationService clothesDetailNavigationService)
            : base(clothesNavigationService)
        {
            Initialize(clothesRestService, clothesDetailNavigationService);

            _clothes = ClothesObservable!.
                       WhereNotNull().
                       Where(clothesResult => clothesResult.OkStatus).
                       Select(clothesResult => clothesResult.Value).
                       ToProperty(this, nameof(Clothes));
            ErrorViewModelObservable = GetErrorViewModel(clothesRestService, clothesDetailNavigationService);

            ClothesFilterCommand = ReactiveCommand.Create<Unit, IReadOnlyList<ClothesViewModelItem>>(_ => GetClothesViewModelItems());
           
            _sortingViewModel = Observable.Return(new SortingViewModel(ClothesFilterCommand)).
                                ToProperty(this, nameof(SortingViewModel));

            _filterViewModel = this.WhenAnyValue(x => x.Clothes, x => x.NavigationParameters, (clothes, parameters) => (clothes, parameters)).
                                    Where(clothesNavigation => clothesNavigation.clothes != null && clothesNavigation.parameters != null).
                                    Select(clothesNavigation => new FilterViewModel(clothesNavigation.parameters!, clothesNavigation.clothes,
                                                                                    ClothesFilterCommand)).
                                    ToProperty(this, nameof(FilterViewModel));

            this.WhenAnyValue(x => x.FilterViewModel).
                 WhereNotNull().
                 Select(_ => Unit.Default).
                 InvokeCommand(this, x => x.ClothesFilterCommand);
            _clothesFiltered = ClothesFilterCommand.ToProperty(this, nameof(ClothesFiltered));

            _clothesViewModelColumnItems = this.WhenAnyValue(x => x.ClothesFiltered).
                                                WhereNotNull().
                                                Select(GetClothesItems).
                                                ToProperty(this, nameof(ClothesViewModelColumnItems));

            ImagesCommand = ReactiveCommand.CreateFromObservable<int, ImageSource>(
                itemIndex => ClothesViewModelColumnItems[itemIndex].ClothesViewModelItemLeft!.
                             ImageCommand.Execute(Unit.Default));

            ChoiceNavigateCommand = ReactiveCommand.CreateFromTask(_ => choiceNavigationService.NavigateTo(new ChoiceNavigationParameters()));
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        private Unit Initialize(IClothesRestService clothesRestService, IClothesDetailNavigationService clothesDetailNavigationService) =>
             this.WhenAnyValue(x => x.NavigationParameters).
                  WhereNotNull().
                  SelectMany(parameters => Observable.FromAsync(() => GetClothes(parameters, clothesRestService, clothesDetailNavigationService),
                                                                RxApp.MainThreadScheduler)).
             Void(clothesObservable => ClothesObservable = clothesObservable).
             Map(_ => Unit.Default);

        /// <summary>
        /// Ошибки при инициализации
        /// </summary>
        public override IObservable<ErrorConnectionViewModel> ErrorViewModelObservable { get; }

        /// <summary>
        /// Одежда
        /// </summary>
        private IObservable<IResultCollection<ClothesViewModelItem>>? ClothesObservable { get; set; }

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
        /// Команда навигации к странице выбора
        /// </summary>
        public ReactiveCommand<Unit, Unit> ChoiceNavigateCommand { get; }

        /// <summary>
        /// Получить модель ошибок
        /// </summary>
        private IObservable<ErrorConnectionViewModel> GetErrorViewModel(IClothesRestService clothesRestService,
                                                                        IClothesDetailNavigationService clothesDetailNavigationService) =>
            ClothesObservable!.
            WhereNotNull().
            Select(clothesResult => new ErrorConnectionViewModel(clothesResult,
                                                                 () => Initialize(clothesRestService, clothesDetailNavigationService)));

        /// <summary>
        /// Получить отфильтрованные модели
        /// </summary>
        private IReadOnlyList<ClothesViewModelItem> GetClothesViewModelItems() =>
            FilterAndSortingViewModelFactory.GetClothesFiltered(Clothes, FilterViewModel.ClothesFilters, SortingViewModel.ClothesSortingType);

        /// <summary> 
        /// Получить модели одежды
        /// </summary>
        private static async Task<IResultCollection<ClothesViewModelItem>> GetClothes(ClothesNavigationParameters? clothesParameters,
                                                                                      IClothesRestService clothesRestService,
                                                                                      IClothesDetailNavigationService clothesDetailNavigationService) =>
            await clothesParameters.ToResultValueNullCheck(new ErrorResult(ErrorResultType.ValueNotFound, nameof(ClothesNavigationParameters))).
            ResultValueBindOkToCollectionAsync(parameters =>
                clothesRestService.GetClothesDetails(parameters.GenderType, parameters.ClothesTypeDomain.Name).
                ResultCollectionOkTaskAsync(clothes =>
                    clothes.Select(clotheItem => new ClothesViewModelItem(clotheItem, parameters.ClothesTypeDomain,
                                                                          clothesRestService, clothesDetailNavigationService))));

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