using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices.ChoiceViewModelItems;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.ViewModels.Profiles
{
    /// <summary>
    /// Модель информации о пользователе
    /// </summary>
    public class ProfileViewModel : NavigationBaseViewModel<ProfileNavigationOptions, IProfileNavigationService>
    {
        public ProfileViewModel(IProfileNavigationService profileNavigationService, IErrorNavigationService errorNavigationService,
                                IProfileRestService profileRestService)
            : base(profileNavigationService, errorNavigationService)
        {
            var profileObservable = GetProfileObservable(profileRestService);
            _profile = GetProfile(profileObservable);
            ToErrorPage(profileObservable);
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
        /// Получить модели выбора одежды
        /// </summary>
        private ObservableAsPropertyHelper<string> GetProfile(IObservable<IResultValue<IBoutiqueUserDomain>> profileObservable) =>
            profileObservable!.
            WhereNotNull().
            Where(profileResult => profileResult.OkStatus).
            Select(profileResult => profileResult.Value.Email).
            ToProperty(this, nameof(Profile));

        /// <summary>
        /// Получить модель личных данных
        /// </summary>
        private static IObservable<IResultValue<IBoutiqueUserDomain>> GetProfileObservable(IProfileRestService profileRestService) =>
             Observable.FromAsync(() => GetProfile(profileRestService), RxApp.MainThreadScheduler);

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private static async Task<IResultValue<IBoutiqueUserDomain>> GetProfile(IProfileRestService profileRestService) =>
            await profileRestService.GetProfile();
    }
}