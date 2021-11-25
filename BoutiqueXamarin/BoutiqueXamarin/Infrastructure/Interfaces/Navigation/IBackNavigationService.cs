using System.Threading.Tasks;
using BoutiqueXamarin.ViewModels.Base;
using Prism.Navigation;
using Xamarin.Forms;

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
        Task<INavigationResult> NavigateBack<TViewModel>(TViewModel viewModel)
           where TViewModel : BaseViewModel;
    }
}