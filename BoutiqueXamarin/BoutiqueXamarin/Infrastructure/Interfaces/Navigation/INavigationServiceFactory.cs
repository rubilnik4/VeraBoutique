using System.Threading.Tasks;
using BoutiqueXamarin.ViewModels.Base;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Сервис навигации
    /// </summary>
    public interface INavigationServiceFactory
    {
        /// <summary>
        /// Перейти назад
        /// </summary>
        Task<INavigationResult> NavigateBack<TViewModel>(TViewModel viewModel)
            where TViewModel : BaseViewModel;
    }
}