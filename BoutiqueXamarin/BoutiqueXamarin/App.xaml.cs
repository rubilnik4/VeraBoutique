using System;
using System.ComponentModel;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueDTO.Factory.RestSharp;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueXamarin.DependencyInjection;
using BoutiqueXamarin.Infrastructure.Implementations;
using BoutiqueXamarin.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarin.Models.Interfaces;
using BoutiqueXamarin.ViewModels;
using BoutiqueXamarin.ViewModels.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.Choice;
using BoutiqueXamarin.Views;
using BoutiqueXamarin.Views.Clothes;
using Prism;
using Prism.Ioc;
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
        /// Инициализация
        /// </summary>
        protected override async void OnInitialized()
        {
            var configManager = Container.Resolve<IConfigManager>();
            string config = configManager.GetConfiguration();

            InitializeComponent();
            //var boutiqueProject = Container.Resolve<IBoutiqueProject>();
            //boutiqueProject.Genders = genders.Value;

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ChoicePage)}");
        }

        /// <summary>
        /// Регистрация типов
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            ProjectRegistration.RegisterProject(containerRegistry);
            PagesRegistration.RegisterPages(containerRegistry);
            ConverterServicesRegistration.RegisterTransferConverters(containerRegistry);
            RestServicesRegistration.RegisterServices(containerRegistry);
            CommonServicesRegistration.RegisterCommonServices(containerRegistry);
        }
    }
}
