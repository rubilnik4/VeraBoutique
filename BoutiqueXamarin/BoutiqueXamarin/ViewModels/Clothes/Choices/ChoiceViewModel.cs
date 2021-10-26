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
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarinCommon.Models.Implementation;
using BoutiqueXamarinCommon.Models.Interfaces;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;
using Prism.Navigation;
using ReactiveUI;
using Splat;

namespace BoutiqueXamarin.ViewModels.Clothes.Choices
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : NavigationLoginViewModel<ChoiceNavigationOptions, IChoiceNavigationService>
    {
        public ChoiceViewModel(IGenderRestService genderRestService, IChoiceNavigationService choiceNavigationService,
                               IClothesNavigationService clothesNavigationService, IProfileNavigationService profileNavigationService)
            : base(choiceNavigationService, profileNavigationService)
        {
            var choiceViewModels = GetChoiceViewModelsObservable(genderRestService, clothesNavigationService);
            _choiceGenderViewModelItems = GetChoiceViewModels(choiceViewModels);
            ToErrorPage(choiceViewModels);
        }
        
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
        /// Получить модели выбора одежды
        /// </summary>
        private ObservableAsPropertyHelper<IList<ChoiceGenderViewModelItem>> GetChoiceViewModels(IObservable<IResultCollection<ChoiceGenderViewModelItem>>? choiceGendersObservable) =>
            choiceGendersObservable!.
            WhereNotNull().
            Where(choiceGendersResult => choiceGendersResult.OkStatus).
            Select(choiceGendersResult => (IList<ChoiceGenderViewModelItem>)choiceGendersResult.Value).
            ToProperty(this, nameof(ChoiceGenderViewModelItems));

        /// <summary>
        /// Получить модели выбора одежды
        /// </summary>
        private static IObservable<IResultCollection<ChoiceGenderViewModelItem>> GetChoiceViewModelsObservable(IGenderRestService genderRestService,
                                                                                                               IClothesNavigationService clothesNavigationService) =>
             Observable.FromAsync(() => GetChoiceGenderItems(clothesNavigationService, genderRestService), RxApp.MainThreadScheduler);

        /// <summary>
        /// Получить модели типа пола одежды
        /// </summary>
        private static async Task<IResultCollection<ChoiceGenderViewModelItem>> GetChoiceGenderItems(IClothesNavigationService clothesNavigationService,
                                                                                                     IGenderRestService genderRestService) =>
            await genderRestService.GetGenderCategories().
            ResultCollectionOkTaskAsync(genderCategories =>
                genderCategories.Select(genderCategory => new ChoiceGenderViewModelItem(clothesNavigationService, genderCategory)));

    }
}