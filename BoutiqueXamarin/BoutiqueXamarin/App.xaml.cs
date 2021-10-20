using System;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueXamarin.DependencyInjection;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Containers;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace BoutiqueXamarin
{
    /// <summary>
    /// ����������
    /// </summary>
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        { }

        /// <summary>
        /// ��������� ������������
        /// </summary>
        private IBoutiqueContainer BoutiqueContainer =>
            new BoutiqueUnityContainer(Container.GetContainer());

        /// <summary>
        /// �������������
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
        /// ����������� �����
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
