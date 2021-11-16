using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Profiles;
using BoutiqueXamarin.ViewModels.Clothes.Choices;
using BoutiqueXamarin.ViewModels.Clothes.Clothes;
using BoutiqueXamarin.ViewModels.Clothes.ClothesDetails;
using BoutiqueXamarin.ViewModels.Profiles;
using BoutiqueXamarin.Views.Clothes.Choices;
using BoutiqueXamarin.Views.Clothes.Clothes;
using BoutiqueXamarin.Views.Clothes.ClothesDetails;
using BoutiqueXamarin.Views.Profiles;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Authorize;
using Prism.Navigation;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;

namespace BoutiqueXamarin.Infrastructure.Implementations.Navigation
{
    /// <summary>
    /// Навигация к странице одежды
    /// </summary>
    public class ClothesNavigationService : NavigationServiceFactory, IClothesNavigationService
    {
        public ClothesNavigationService(INavigationService navigationService, ILoginService loginService,
                                        IClothesRestService clothesRestService, IGenderRestService genderRestService)
            : base(navigationService, loginService)
        {
            _clothesRestService = clothesRestService;
            _genderRestService = genderRestService;
        }

        /// <summary>
        /// Сервис одежды
        /// </summary>
        private readonly IClothesRestService _clothesRestService;

        /// <summary>
        /// Сервис типа пола
        /// </summary>
        private readonly IGenderRestService _genderRestService;

        /// <summary>
        /// Перейти к странице одежды
        /// </summary>
        public async Task<INavigationResult> ToClothesPage(GenderType genderType, IClothesTypeDomain clothesTypeDomain) =>
            await _clothesRestService.GetClothesDetails(genderType, clothesTypeDomain.Name).
            ToResultValueTaskAsync().
            ResultValueToValueOkBadBindAsync(clothesDetails => ToClothesPage(clothesDetails, clothesTypeDomain.SizeTypeDefault),
                                             errors => OnErrorNavigate(errors, () => ToClothesPage(genderType, clothesTypeDomain)));

        /// <summary>
        /// Перейти к странице информации одежды
        /// </summary>
        public async Task<INavigationResult> ToClothesDetailPage(IClothesDetailDomain clothesDetail, SizeType defaultSizeType) =>
            await new ClothesDetailNavigationOptions(clothesDetail, defaultSizeType).
            MapAsync(NavigateTo<ClothesDetailPage, ClothesDetailViewModel, ClothesDetailNavigationOptions>);

        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        public async Task<INavigationResult> ToChoicePage() =>
            await _genderRestService.GetGenderCategories().
            ToResultValueTaskAsync().
            ResultValueToValueOkBadBindAsync(ToChoicePage,
                                             errors => OnErrorNavigate(errors, ToChoicePage));

        /// <summary>
        /// Перейти к странице одежды
        /// </summary>
        private async Task<INavigationResult> ToClothesPage(IReadOnlyCollection<IClothesDetailDomain> clothesDetails, SizeType sizeTypeDefault) =>
            await new ClothesNavigationOptions(clothesDetails, sizeTypeDefault).
            MapAsync(NavigateTo<ClothesPage, ClothesViewModel, ClothesNavigationOptions>);

        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        private async Task<INavigationResult> ToChoicePage(IReadOnlyCollection<IGenderCategoryDomain> genderCategories) =>
            await new ChoiceNavigationOptions(genderCategories).
            MapAsync(NavigateTo<ChoicePage, ChoiceViewModel, ChoiceNavigationOptions>);
    }
}