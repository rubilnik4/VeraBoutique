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
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesFiltersViewModelItems;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems.ClothesSortingViewModelItems;
using BoutiqueXamarin.ViewModels.Interfaces.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Results;
using Prism.Common;
using Prism.Navigation;
using ReactiveUI;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public class ClothesViewModel : NavigationViewModel<ClothesNavigationOptions>, INavigationProfileViewModel
    {
        public ClothesViewModel(IClothesRestService clothesRestService, INavigationServiceFactory navigationServiceFactory,
                                IClothesNavigationService clothesNavigationService, IProfileNavigationService profileNavigationService)
            : base(navigationServiceFactory)
        {
            _clothesRestService = clothesRestService;
            _clothesNavigationService = clothesNavigationService;
            UserRightMenuViewModel = new UserRightMenuViewModel(profileNavigationService);

            _clothes = GetClothesObservable();

            ClothesFilterCommand = ReactiveCommand.Create<Unit, IReadOnlyList<ClothesViewModelItem>>(_ => GetClothesViewModelItems());
            SortingViewModel = new SortingViewModel(ClothesFilterCommand);
            _filterViewModel = GetFilterViewModel();

            FilterCommandExecute();
            _clothesFiltered = ClothesFilterCommand.ToProperty(this, nameof(ClothesFiltered));
            _clothesViewModelColumnItems = GetClothesColumns();

            ImagesCommand = ReactiveCommand.CreateFromObservable<int, ImageSource>(GetImageObservable);
            ChoiceNavigateCommand = ReactiveCommand.CreateFromTask(_ => clothesNavigationService.ToChoicePage());
        }

        /// <summary>
        /// Сервис одежды
        /// </summary>
        private readonly IClothesRestService _clothesRestService;

        /// <summary>
        /// Навигация к странице информации об одежде
        /// </summary>
        private readonly IClothesNavigationService _clothesNavigationService;

        /// <summary>
        /// Правое меню пользователя
        /// </summary>
        public UserRightMenuViewModel UserRightMenuViewModel { get; }

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
        public SortingViewModel SortingViewModel { get; }

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
        public ReactiveCommand<Unit, INavigationResult> ChoiceNavigateCommand { get; }

        /// <summary>
        /// Получить модели одежды
        /// </summary>
        private ObservableAsPropertyHelper<IReadOnlyCollection<ClothesViewModelItem>> GetClothesObservable() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(options =>ClothesViewModelFactory.GetClothes(options, _clothesRestService, _clothesNavigationService)).
                 ToProperty(this, nameof(Clothes));

        /// <summary>
        /// Получить модель фильтрации
        /// </summary>
        private ObservableAsPropertyHelper<FilterViewModel> GetFilterViewModel() =>
            this.WhenAnyValue(x => x.Clothes, x => x.NavigationOptions, (clothes, options) => (clothes, options)).
                 Where(clothesNavigation => clothesNavigation.clothes != null && clothesNavigation.options != null).
                 Select(clothesNavigation => new FilterViewModel(clothesNavigation.options!, clothesNavigation.clothes, ClothesFilterCommand)).
                 ToProperty(this, nameof(FilterViewModel));

        /// <summary>
        /// Получить колонки с одеждой
        /// </summary>
        private ObservableAsPropertyHelper<IReadOnlyList<ClothesColumnViewModelItem>> GetClothesColumns() =>
            this.WhenAnyValue(x => x.ClothesFiltered).
                 WhereNotNull().
                 Select(ClothesViewModelFactory.GetClothesItems).
                 ToProperty(this, nameof(ClothesViewModelColumnItems));

        /// <summary>
        /// Отфильтровать список
        /// </summary>
        private void FilterCommandExecute() =>
              this.WhenAnyValue(x => x.FilterViewModel).
                   WhereNotNull().
                   Select(_ => Unit.Default).
                   InvokeCommand(this, x => x.ClothesFilterCommand);

        /// <summary>
        /// Получить отфильтрованные модели
        /// </summary>
        private IReadOnlyList<ClothesViewModelItem> GetClothesViewModelItems() =>
            FilterAndSortingViewModelFactory.GetClothesFiltered(Clothes, FilterViewModel.ClothesFilters, SortingViewModel.ClothesSortingType);

        /// <summary>
        /// Получить изображение
        /// </summary>
        private IObservable<ImageSource> GetImageObservable(int itemIndex) =>
             ClothesViewModelColumnItems[itemIndex].ClothesViewModelItemLeft!.ImageCommand.Execute(Unit.Default);
    }
}