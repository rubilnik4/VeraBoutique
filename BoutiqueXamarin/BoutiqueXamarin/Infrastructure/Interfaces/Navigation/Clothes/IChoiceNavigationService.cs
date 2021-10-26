using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes
{
    /// <summary>
    /// Сервис навигации к странице выбора одежды
    /// </summary>
    public interface IChoiceNavigationService : IBaseNavigationService<ChoiceNavigationOptions>
    { }
}