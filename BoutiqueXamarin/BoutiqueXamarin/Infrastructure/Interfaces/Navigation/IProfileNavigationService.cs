using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Навигация к странице пользовательской информации
    /// </summary>
    public interface IProfileNavigationService: INavigationServiceFactory
    {
        /// <summary>
        /// Перейти к странице авторизации
        /// </summary>
        Task<INavigationResult> ToLoginPage();

        /// <summary>
        /// Перейти к странице регистрации
        /// </summary>
        Task<INavigationResult> ToRegisterPage();

        /// <summary>
        /// Перейти к странице личной информации
        /// </summary>
        Task <INavigationResult> ToProfilePage();

        /// <summary>
        /// Перейти к странице личных данных
        /// </summary>
        Task<INavigationResult> ToPersonalPage(IBoutiqueUserDomain user);
    }
}