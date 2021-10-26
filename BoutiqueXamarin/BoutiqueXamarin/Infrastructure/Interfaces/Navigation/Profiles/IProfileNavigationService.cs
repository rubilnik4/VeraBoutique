using System.Threading.Tasks;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles
{
    /// <summary>
    /// Сервис навигации к странице информации о пользователе
    /// </summary>
    public interface IProfileNavigationService : IBaseNavigationService<ProfileNavigationOptions>
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task<INavigationResult> NavigateTo();
    }
}