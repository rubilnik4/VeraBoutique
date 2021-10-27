using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Implementations.Navigation.Base;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Errors;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Errors;
using BoutiqueXamarin.Views.Clothes.Clothes;
using BoutiqueXamarin.Views.Errors;
using Prism.Navigation;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation.Errors
{
    /// <summary>
    /// Сервис навигации к странице ошибок
    /// </summary>
    public class ErrorNavigationService : BaseNavigationService<ErrorNavigationOptions, ErrorPage>, IErrorNavigationService
    {
        public ErrorNavigationService(INavigationService navigationService)
            : base(navigationService)
        { }

        /// <summary>
        /// Перейти к странице
        /// </summary>
        public async Task<INavigationResult> NavigateTo(IReadOnlyCollection<IErrorResult> errors) =>
            await NavigateTo(new ErrorNavigationOptions(errors));
    }
}