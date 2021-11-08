using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Base.MenuItems;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarin.ViewModels.Interfaces.Base;
using BoutiqueXamarinCommon.Models.Implementation;
using BoutiqueXamarinCommon.Models.Interfaces;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;
using Prism.Navigation;
using ReactiveUI;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : NavigationViewModel<ChoiceNavigationOptions>, INavigationProfileViewModel
    {
        public ChoiceViewModel(INavigationServiceFactory navigationServiceFactory, IProfileNavigationService profileNavigationService,
                               IClothesNavigationService clothesNavigationService)
            : base(navigationServiceFactory)
        {
            _clothesNavigationService = clothesNavigationService;
            UserRightMenuViewModel = new UserRightMenuViewModel(profileNavigationService);
            _choiceGenderViewModelItems = GetChoiceViewModels();
        }

        /// <summary>
        /// Навигация к странице одежды
        /// </summary>
        private readonly IClothesNavigationService _clothesNavigationService;

        /// <summary>
        /// Правое меню пользователя
        /// </summary>
        public UserRightMenuViewModel UserRightMenuViewModel { get; }

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        private readonly ObservableAsPropertyHelper<IList<ChoiceGenderViewModelItem>> _choiceGenderViewModelItems;

        /// <summary>
        /// Модели типа пола одежды
        /// </summary>
        public IList<ChoiceGenderViewModelItem> ChoiceGenderViewModelItems =>
            _choiceGenderViewModelItems.Value;

        /// <summary>
        /// Выбранный тип пола
        /// </summary>
        private ChoiceGenderViewModelItem? _selectedGenderViewModelItem;

        /// <summary>
        /// Выбранный тип пола
        /// </summary>
        public ChoiceGenderViewModelItem? SelectedGenderViewModelItem
        {
            get => _selectedGenderViewModelItem;
            set => this.RaiseAndSetIfChanged(ref _selectedGenderViewModelItem, value);
        }

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private ObservableAsPropertyHelper<IList<ChoiceGenderViewModelItem>> GetChoiceViewModels() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(options => options.GenderCategories).
                 Select(genderCategories => GetChoiceGenderItems(genderCategories, _clothesNavigationService)).
                 ToProperty(this, nameof(ChoiceGenderViewModelItems));

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static IList<ChoiceGenderViewModelItem> GetChoiceGenderItems(IEnumerable<IGenderCategoryDomain> genderCategories,
                                                                             IClothesNavigationService clothesNavigationService) =>
            genderCategories.
            Select(genderCategory => new ChoiceGenderViewModelItem(clothesNavigationService, genderCategory)).
            ToList();

    }
}