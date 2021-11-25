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
        public ProfileViewModel(IProfileNavigationService profileNavigationService)
            : base(profileNavigationService)
        {
            _profile = GetProfile();
            PersonalCommand = ReactiveCommand.CreateFromTask<IBoutiqueUserDomain, INavigationResult>(profileNavigationService.ToPersonalPage);
            LogoutCommand = ReactiveCommand.CreateFromTask<Unit, INavigationResult>(_ => profileNavigationService.ToLoginPage());
        }

        /// <summary>
        /// Модель личных данных
        /// </summary>
        private readonly ObservableAsPropertyHelper<IBoutiqueUserDomain> _profile;

        /// <summary>
        /// Модель личных данных
        /// </summary>
        public IBoutiqueUserDomain Profile =>
            _profile.Value;

        /// <summary>
        /// Команда перехода к личной информации
        /// </summary>
        public ReactiveCommand<IBoutiqueUserDomain, INavigationResult> PersonalCommand { get; }

        /// <summary>
        /// Команда выхода из профиля
        /// </summary>
        public ReactiveCommand<Unit, INavigationResult> LogoutCommand { get; }

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private ObservableAsPropertyHelper<IBoutiqueUserDomain> GetProfile() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(options => options.User).
                 ToProperty(this, nameof(Profile));
    }
}