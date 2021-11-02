using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Profiles;
using Prism.Navigation;
using ReactiveUI.XamForms;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Сервис навигации
    /// </summary>
    public interface INavigationServiceFactory: IBackNavigationService
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task<INavigationResult> ToErrorPage(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Перейти к странице авторизации
        /// </summary>
        Task<INavigationResult> ToLoginPage();

        /// <summary>
        /// Перейти к странице регистрации
        /// </summary>
        Task<INavigationResult> ToRegisterPage();

        ///// <summary>
        ///// Перейти к странице выбора одежды
        ///// </summary>
        //Task <INavigationResult> ToChoicePage();

        ///// <summary>
        ///// Перейти к странице одежды
        ///// </summary>
        //Task<INavigationResult> ToClothesPage(GenderType genderType, IClothesTypeDomain clothesTypeDomain);

        ///// <summary>
        ///// Перейти к странице информации об одежде
        ///// </summary>
        //Task<INavigationResult> ToClothesDetailPage(IClothesDetailDomain clothesDetail, SizeType defaultSizeType);

       
    }
}