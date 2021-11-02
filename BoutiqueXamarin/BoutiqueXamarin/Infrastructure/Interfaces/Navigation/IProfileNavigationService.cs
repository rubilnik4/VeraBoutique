using System.Threading.Tasks;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Навигация к странице пользовательской информации
    /// </summary>
    public interface IProfileNavigationService: INavigationServiceFactory
    {
        /// <summary>
        /// Перейти к странице личной информации
        /// </summary>
        Task<INavigationResult> ToProfilePage();
    }
}