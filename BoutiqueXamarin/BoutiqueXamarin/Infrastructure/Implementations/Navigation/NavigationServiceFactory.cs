using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Authorizes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails;
using BoutiqueXamarin.ViewModels.Errors;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Authorizes;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Clothes;
using BoutiqueXamarin.Views.Clothes.ClothesDetails;
using BoutiqueXamarin.Views.Errors;
using BoutiqueXamarin.Views.Profiles;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI.XamForms;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using Xamarin.Forms;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    public class NavigationServiceFactory: INavigationServiceFactory
    {
        public NavigationServiceFactory(INavigationService navigationService, ILoginStore loginStore)
        {
            _navigationService = navigationService;
            _loginStore = loginStore;
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Хранение и загрузка данных аутентификации
        /// </summary>
        private readonly ILoginStore _loginStore;

        /// <summary>
        /// Перейти к странице ошибок
        /// </summary>
        public async Task<INavigationResult> ToErrorPage(IEnumerable<IErrorResult> errors) =>
            await new ErrorNavigationOptions(errors).
            MapAsync(NavigateTo<ErrorPage, ErrorViewModel, ErrorNavigationOptions>);

        /// <summary>
        /// Перейти к странице авторизации
        /// </summary>
        public async Task<INavigationResult> ToLoginPage() =>
            await new LoginNavigationOptions().
            MapAsync(NavigateTo<LoginPage, LoginViewModel, LoginNavigationOptions>);

        /// <summary>
        /// Перейти к странице регистрации
        /// </summary>
        public async Task<INavigationResult> ToRegisterPage() =>
            await new RegisterNavigationOptions().
            MapAsync(NavigateTo<RegisterPage, RegisterViewModel, RegisterNavigationOptions>);

        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        public async Task<INavigationResult> ToChoicePage() =>
            await new ChoiceNavigationOptions().
            MapAsync(NavigateTo<ChoicePage, ChoiceViewModel, ChoiceNavigationOptions>);

        /// <summary>
        /// Перейти к странице одежды
        /// </summary>
        public async Task<INavigationResult> ToClothesPage(GenderType genderType, IClothesTypeDomain clothesTypeDomain) =>
            await new ClothesNavigationOptions(genderType, clothesTypeDomain).
            MapAsync(NavigateTo<ClothesPage, ClothesViewModel, ClothesNavigationOptions>);

        /// <summary>
        /// Перейти к странице информации об одежде
        /// </summary>
        public async Task<INavigationResult> ToClothesDetailPage(IClothesDetailDomain clothesDetail, SizeType defaultSizeType) =>
            await new ClothesDetailNavigationOptions(clothesDetail, defaultSizeType).
            MapAsync(NavigateTo<ClothesDetailPage, ClothesDetailViewModel, ClothesDetailNavigationOptions>);

        /// <summary>
        /// Перейти к странице личной информации
        /// </summary>
        public async Task<INavigationResult> ToProfilePage() =>
            await ToAuthorizePage(token => new ProfileNavigationOptions(token).
                                           MapAsync(NavigateTo<ProfilePage, ProfileViewModel, ProfileNavigationOptions>));

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task<INavigationResult> NavigateTo<TPage, TViewModel, TOption>(TOption options)
            where TPage : ReactiveContentPage<TViewModel>
            where TViewModel : NavigationViewModel<TOption>
            where TOption : BaseNavigationOptions =>
            await new NavigationParameters {{GetOptionsName<TOption>(), options}}.
            MapAsync(navigationParameters => _navigationService.NavigateAsync(GetPageName<TPage>(), navigationParameters));

        /// <summary>
        /// Перейти назад
        /// </summary>
        public Task<INavigationResult> NavigateBack() =>
            _navigationService.GoBackAsync();

        /// <summary>
        /// Перейти к странице с авторизацией
        /// </summary>
        private async Task<INavigationResult> ToAuthorizePage(Func<string, Task<INavigationResult>> navigateFunc) =>
            await _loginStore.GetToken().
            WhereContinueBindAsync(result => result.OkStatus,
                                   result => navigateFunc(result.Value),
                                   result => ToLoginPage());

        /// <summary>
        /// Имя параметра навигации
        /// </summary>
        public static string GetOptionsName<TOption>()
            where TOption : BaseNavigationOptions =>
            typeof(TOption).Name;

        /// <summary>
        /// Имя страницы для навигации
        /// </summary>
        private static string GetPageName<TPage>()
            where TPage : Page =>
            typeof(TPage).Name;
    }
}