using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base
{
    /// <summary>
    /// Сервис навигации назад
    /// </summary>
    public abstract class BackNavigationService: IBackNavigationService
    {
        protected BackNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// Сервис навигации Prism
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Перейти назад
        /// </summary>
        public async Task NavigateBack() =>
            await _navigationService.GoBackAsync();
    }
}