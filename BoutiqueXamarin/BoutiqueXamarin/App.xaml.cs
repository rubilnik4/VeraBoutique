using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueXamarin.DependencyInjection;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Containers;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using ResultFunctional.FunctionalExtensions.Async;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace BoutiqueXamarin
{
    /// <summary>
    /// Приложение
    /// </summary>
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
         
        }

        private void Init()
        {
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();
            XF.Material.Forms.Material.Use("Material.Style");
        }

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
            Void(_ => Init()).
            ResultValueVoidOkBadAsync(
                _ => BoutiqueContainer.Resolve<INavigationServiceFactory>().ToInitialPage(),
                errors => BoutiqueContainer.Resolve<INavigationServiceFactory>().ToErrorPage(errors));

        /// <summary>
        /// Регистрация типов
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            PagesRegistration.RegisterPages(containerRegistry);
            ConverterServicesRegistration.RegisterTransferConverters(BoutiqueContainer);
            CommonServicesRegistration.RegisterCommonServices(BoutiqueContainer);
            AuthorizeServicesRegistration.RegisterAuthorizeServices(BoutiqueContainer);
            NavigationServicesRegistration.RegisterNavigationServices(BoutiqueContainer);
        }
    }
}
