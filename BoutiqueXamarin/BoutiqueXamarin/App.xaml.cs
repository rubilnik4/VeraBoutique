using BoutiqueXamarin.ViewModels;
using BoutiqueXamarin.ViewModels.Clothes;
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
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ChoicePage)}");
        }

        /// <summary>
        /// Регистрация типов
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            RegisterPages(containerRegistry);
        }

        /// <summary>
        /// Регистрация видов и моделей
        /// </summary>
        private static void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChoicePage, ChoiceViewModel>();
        }
    }
}
