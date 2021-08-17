using System.Threading.Tasks;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base
{
    /// <summary>
    /// Сервис навигации назад
    /// </summary>
    public interface IBackNavigationService
    {
        /// <summary>
        /// Перейти назад
        /// </summary>
        Task NavigateBack();
    }
}