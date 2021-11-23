using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Authorizes.RegisterViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ReactiveUI;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Forms.Internals;

namespace BoutiqueXamarin.ViewModels.Profiles
{
    /// <summary>
    /// Личная информация
    /// </summary>
    public class PersonalViewModel : NavigationViewModel<PersonalNavigationOptions>
    {
        public PersonalViewModel(IUserRestService userRestService, IProfileNavigationService profileNavigationService)
          : base(profileNavigationService)
        {
            _profile = GetProfile();
            _registerPersonalViewModel = GetRegisterPersonal();
            UpdateCommand = ReactiveCommand.CreateFromTask<RegisterPersonalViewModel, IResultError>(
                                registerPersonal => UpdatePersonal(Profile, registerPersonal, userRestService, profileNavigationService));
            _personalErrors = UpdateCommand.ToProperty(this, nameof(PersonalErrors), scheduler: RxApp.MainThreadScheduler);
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
        /// Модель личных данных
        /// </summary>
        private readonly ObservableAsPropertyHelper<RegisterPersonalViewModel> _registerPersonalViewModel;

        /// <summary>
        /// Модель личных данных
        /// </summary>
        public RegisterPersonalViewModel RegisterPersonalViewModel =>
            _registerPersonalViewModel.Value;

        /// <summary>
        /// Ошибки авторизации
        /// </summary>
        private readonly ObservableAsPropertyHelper<IResultError> _personalErrors;

        /// <summary>
        /// Ошибки авторизации
        /// </summary>
        public IResultError PersonalErrors =>
            _personalErrors.Value;

        /// <summary>
        /// Команда обновления личных данных
        /// </summary>
        public ReactiveCommand<RegisterPersonalViewModel, IResultError> UpdateCommand { get; }

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private ObservableAsPropertyHelper<IBoutiqueUserDomain> GetProfile() =>
            this.WhenAnyValue(x => x.NavigationOptions).
                 WhereNotNull().
                 Select(options => options.User).
                 ToProperty(this, nameof(Profile));

        /// <summary>
        /// Получить страницу личных данных
        /// </summary>
        private ObservableAsPropertyHelper<RegisterPersonalViewModel> GetRegisterPersonal() =>
            this.WhenAnyValue(x => x.Profile).
                 WhereNotNull().
                 Select(user => new RegisterPersonalViewModel(user.Name, user.Surname, user.Address, user.Phone)).
                 ToProperty(this, nameof(RegisterPersonalViewModel));

        /// <summary>
        /// Обновить личные данные
        /// </summary>
        private static async Task<IResultError> UpdatePersonal(IBoutiqueUserDomain profile, RegisterPersonalViewModel registerPersonalViewModel,
                                                               IUserRestService userRestService, IProfileNavigationService profileNavigationService) =>
            await registerPersonalViewModel.RegisterPersonalCommand.
            Execute(registerPersonalViewModel.RegisterPersonalValidation).ToTask().
            ToResultValueTaskAsync(profile).
            ResultValueOkTaskAsync(user => user.UpdatePersonal(registerPersonalViewModel.Name, registerPersonalViewModel.Surname,
                                                               registerPersonalViewModel.Address, registerPersonalViewModel.Phone)).
            ResultValueBindErrorsOkBindAsync(userRestService.UpdateUser).
            ResultValueOkBindAsync(_ => profileNavigationService.ToProfilePage());
    }
}