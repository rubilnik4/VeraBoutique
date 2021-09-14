using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.ViewModels.Base;

namespace BoutiqueXamarin.ViewModels.Authorizes
{
    /// <summary>
    /// Модель регистрации
    /// </summary>
    public class RegisterViewModel : NavigationBaseViewModel<RegisterNavigationParameters, IRegisterNavigationService>
    {
        public RegisterViewModel(IRegisterNavigationService registerNavigationService)
         : base(registerNavigationService)
        {
        
        }
    }
}