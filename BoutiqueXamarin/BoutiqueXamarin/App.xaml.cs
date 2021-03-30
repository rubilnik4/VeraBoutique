using System;
using System.ComponentModel;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.DependencyInjection;
using BoutiqueXamarin.Views;
using BoutiqueXamarin.Views.Clothes;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Containers;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using RestSharp;
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
            ResultErrorVoidOk(() => RestServicesRegistration.RegisterServices(BoutiqueContainer)).
            ResultErrorBindOk(() => ProjectRegistration.RegisterProject(BoutiqueContainer)).
            Void(_ => InitializeComponent()).
            ResultErrorVoidOkBadAsync(
                actionOk: () => NavigationService.NavigateAsync(nameof(ChoicePage)),
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
        }
    }
}
