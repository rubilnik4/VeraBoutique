using System.Threading.Tasks;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Навигация к странице выбора одежды
    /// </summary>
    public interface IChoiceNavigationService
    {
        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        Task<INavigationResult> ToChoicePage();
    }
}