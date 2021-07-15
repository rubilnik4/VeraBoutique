using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Choices.Tabs;
using BoutiqueXamarin.Views.Clothes.Clothes;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Clothes
{
    /// <summary>
    /// Сервис навигации к странице выбора одежды
    /// </summary>
    public class ChoiceNavigationService : BaseNavigationService<ChoiceNavigationParameters, ChoicePage>,
                                           IChoiceNavigationService
    {
        public ChoiceNavigationService(INavigationService navigationService)
            : base(navigationService)
        { }
    }
}