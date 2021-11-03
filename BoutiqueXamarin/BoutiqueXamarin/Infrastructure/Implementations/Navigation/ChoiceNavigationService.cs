using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    /// <summary>
    /// Навигация к странице выбора одежды
    /// </summary>
    public class ChoiceNavigationService : NavigationServiceFactory, IChoiceNavigationService
    {
        public ChoiceNavigationService(INavigationService navigationService, ILoginService loginService,
                                       IGenderRestService genderRestService)
             : base(navigationService, loginService)
        {
            _genderRestService = genderRestService;
        }

        /// <summary>
        /// Сервис типа пола
        /// </summary>
        private readonly IGenderRestService _genderRestService;

        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        public async Task<INavigationResult> ToChoicePage() =>
            await _genderRestService.GetGenderCategories().
            ResultValueToValueOkBadBindAsync(ToChoicePage,
                                             OnErrorNavigate);

        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        private async Task<INavigationResult> ToChoicePage(IReadOnlyCollection<IGenderCategoryDomain> genderCategories) =>
            await new ChoiceNavigationOptions(genderCategories).
            MapAsync(NavigateTo<ChoicePage, ChoiceViewModel, ChoiceNavigationOptions>);
    }
}