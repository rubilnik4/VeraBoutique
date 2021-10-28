using System.Reactive.Linq;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Authorizes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Profiles;
using BoutiqueXamarin.Models.Implementations.Navigation.Authorize;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.ViewModels.Authorizes.AuthorizeViewModelItems;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using ReactiveUI;
using ResultFunctional.Models.Interfaces.Results;
using Xamarin.Essentials;

namespace BoutiqueXamarin.ViewModels.Errors
{
    /// <summary>
    /// Ошибки
    /// </summary>
    public class ErrorViewModel : NavigationViewModel<ErrorNavigationOptions, IErrorNavigationService>
    {
        public ErrorViewModel(IErrorNavigationService errorNavigationService)
         : base(errorNavigationService)
        { }
    }
}