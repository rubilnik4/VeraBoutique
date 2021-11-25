using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Base;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Clothes;
using BoutiqueXamarin.Views.Profiles;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    /// <summary>
    /// Сервис навигации назад
    /// </summary>
    public class BackNavigationService: IBackNavigationService
    {
        public BackNavigationService(INavigationService navigationService, INavigationHistoryService navigationHistoryService)
        {
            _navigationService = navigationService;
            _navigationHistoryService = navigationHistoryService;
        }

        /// <summary>
        /// Сервис навигации
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// История навигации
        /// </summary>
        private readonly INavigationHistoryService _navigationHistoryService;

        /// <summary>
        /// Перейти назад
        /// </summary>
        public async Task<INavigationResult> NavigateBack<TViewModel>(TViewModel viewModel)
            where TViewModel : BaseViewModel =>
            viewModel switch
            {
                PersonalViewModel _ => await NavigateBack<ProfilePage, ProfileViewModel, ProfileNavigationOptions>(),
                ProfileViewModel _ => await NavigateBack<ChoicePage, ChoiceViewModel, ChoiceNavigationOptions>(),
                ClothesViewModel _ => await NavigateBack<ChoicePage, ChoiceViewModel, ChoiceNavigationOptions>(),
                ClothesDetailViewModel _ => await NavigateBack<ClothesPage, ClothesViewModel, ClothesNavigationOptions>(),
                _ => await _navigationService.GoBackAsync(),
            };

        /// <summary>
        /// Перейти назад
        /// </summary>
        private async Task<INavigationResult> NavigateBack<TPage, TViewModel, TOption>()
            where TPage : NavigationBasePage<TViewModel, TOption>
            where TViewModel : NavigationViewModel<TOption>
            where TOption : BaseNavigationOptions =>
            await _navigationHistoryService.DequeueHistory<TOption>().
            Map(NavigateFunctions.ToNavigationParameters).
            MapAsync(parameters => _navigationService.NavigateAsync(NavigateFunctions.GetPageName<TPage>(), parameters));
    }
}