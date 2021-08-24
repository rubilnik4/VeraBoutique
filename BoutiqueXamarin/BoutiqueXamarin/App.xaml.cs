using System;
using System.ComponentModel;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.DependencyInjection;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Views;
using BoutiqueXamarin.Views.Clothes;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Containers;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace BoutiqueXamarin
{
    /// <summary>
    /// Приложение
    /// </summary>
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        { }

        /// <summary>
        /// Контейнер зависимостей
        /// </summary>
        private IBoutiqueContainer BoutiqueContainer =>
            new BoutiqueUnityContainer(Container.GetContainer());

        /// <summary>
        /// Инициализация
        /// </summary>
        protected override async void OnInitialized() =>
            await ConfigurationRegistration.RegisterConfiguration(BoutiqueContainer).
            ResultValueVoidOk(configuration => RestServicesRegistration.RegisterServices(BoutiqueContainer, configuration)).
            ResultValueBindErrorsOk(_ => ProjectRegistration.RegisterProject(BoutiqueContainer)).
            Void(_ => InitializeComponent()).
            ResultValueVoidOkBadAsync(
                actionOk: _ => BoutiqueContainer.Resolve<IChoiceNavigationService>().NavigateTo(new ChoiceNavigationParameters()),
                actionBad: errors => throw new NotImplementedException());

        /// <summary>
        /// Регистрация типов
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            PagesRegistration.RegisterPages(containerRegistry);
            ConverterServicesRegistration.RegisterTransferConverters(BoutiqueContainer);
            CommonServicesRegistration.RegisterCommonServices(BoutiqueContainer);
            NavigationServicesRegistration.RegisterNavigationServices(BoutiqueContainer);
        }
    }
}
