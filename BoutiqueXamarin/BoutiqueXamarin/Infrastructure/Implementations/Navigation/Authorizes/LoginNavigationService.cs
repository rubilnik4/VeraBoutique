using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Views.Authorizes;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Authorizes
{
    /// <summary>
    /// Сервис навигации к странице авторизации
    /// </summary>
    public class LoginNavigationService : BaseNavigationService<LoginNavigationParameters, LoginPage>, ILoginNavigationService
    {
        public LoginNavigationService(INavigationService navigationService)
            : base(navigationService)
        { }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task NavigateTo() =>
            await NavigateTo(new LoginNavigationParameters());
    }
}