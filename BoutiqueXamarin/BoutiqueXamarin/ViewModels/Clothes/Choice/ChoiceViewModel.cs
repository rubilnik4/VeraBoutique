using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Choice
{
    /// <summary>
    /// Выбор типа одежды
    /// </summary>
    public class ChoiceViewModel : ViewModelBase
    {
        public ChoiceViewModel(INavigationService navigationService, IGenderRestService genderRestService)
          : base(navigationService)
        {
            var genders = genderRestService.Get().Result;
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Выбор категории";
    }
}