using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails.ClothesDetailViewModelItems;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Profiles
{
    /// <summary>
    /// Модель информации о пользователе
    /// </summary>
    public class ProfileViewModel : NavigationViewModel<ProfileNavigationOptions>
    {
        public ProfileViewModel(INavigationServiceFactory navigationServiceFactory)
            : base(navigationServiceFactory)
        {
            _profile = GetProfile();
            LogoutCommand = ReactiveCommand.CreateFromTask<Unit, INavigationResult>(_ => Logout(navigationServiceFactory));
        }


        /// <summary>
        /// Модель личных данных
        /// </summary>
        private readonly ObservableAsPropertyHelper<string> _profile;

        /// <summary>
        /// Модель личных данных
        /// </summary>
        public string Profile =>
            _profile.Value;

        /// <summary>
        /// Команда выхода из профиля
        /// </summary>
        public ReactiveCommand<Unit, INavigationResult> LogoutCommand { get; }

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private ObservableAsPropertyHelper<string> GetProfile() =>
            this.WhenAnyValue(x => x.NavigationParameters).
                 WhereNotNull().
                 Select(options => options.User.Email).
                 ToProperty(this, nameof(Profile));

        /// <summary>
        /// Выйти из профиля
        /// </summary>
        private static async Task<INavigationResult> Logout(INavigationServiceFactory navigationServiceFactory) =>
            await navigationServiceFactory.
            MapAsync(_ => navigationServiceFactory.ToLoginPage());
    }
}