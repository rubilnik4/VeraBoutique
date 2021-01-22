using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using Prism.Commands;
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
            _genderRestService = genderRestService;
        }

        private readonly IGenderRestService _genderRestService;

        /// <summary>
        /// Заголовок
        /// </summary>
        public override string Title => "Выбор категории";

        /// <summary>
        /// Параметры перехода с формы
        /// </summary>
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var genders = await _genderRestService.Get();
        }
    }
}