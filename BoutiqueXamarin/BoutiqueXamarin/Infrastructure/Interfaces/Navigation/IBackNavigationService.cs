using System.Threading.Tasks;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Сервис навигации назад
    /// </summary>
    public interface IBackNavigationService
    {
        /// <summary>
        /// Перейти назад
        /// </summary>
        Task<INavigationResult> NavigateBack();
    }
}