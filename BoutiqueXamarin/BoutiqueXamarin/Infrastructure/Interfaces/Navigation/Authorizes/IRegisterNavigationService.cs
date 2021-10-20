using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes
{
    /// <summary>
    /// Сервис навигации к странице регистрации
    /// </summary>
    public interface IRegisterNavigationService : IBaseNavigationService<RegisterNavigationParameters>
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task<INavigationResult> NavigateTo();
    }
}