using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems
{
    /// <summary>
    /// Получение данных одежды
    /// </summary>
    public static class ClothesViewModelFactory
    {
        /// <summary> 
        /// Получить модели одежды
        /// </summary>
        public static async Task<IResultCollection<ClothesViewModelItem>> GetClothes(ClothesNavigationParameters? clothesParameters,
                                                                                      IClothesRestService clothesRestService,
                                                                                      IClothesDetailNavigationService clothesDetailNavigationService) =>
            await clothesParameters.ToResultValueNullCheck(new ErrorResult(ErrorResultType.ValueNotFound, nameof(ClothesNavigationParameters))).
            ResultValueBindOkToCollectionAsync(parameters =>
                clothesRestService.GetClothesDetails(parameters.GenderType, parameters.ClothesTypeDomain.Name).
                ResultCollectionOkTaskAsync(clothes =>
                    clothes.Select(clotheItem => new ClothesViewModelItem(clotheItem, parameters.ClothesTypeDomain,
                                                                          clothesRestService, clothesDetailNavigationService))));

        /// <summary>
        /// Преобразовать в модели одежды
        /// </summary>
        public static IReadOnlyList<ClothesColumnViewModelItem> GetClothesItems(IReadOnlyList<ClothesViewModelItem> clothesViewModels) =>
            clothesViewModels.
            Map(clothesItems => (columnLeft: clothesItems.Where((clothes, index) => index % 2 == 0),
                                 columnRight: clothesItems.Where((clothes, index) => index % 2 != 0))).
            Map(clothesPair => clothesPair.columnLeft.ZipLong(clothesPair.columnRight,
                                                              (first, second) => new ClothesColumnViewModelItem(first, second))).
            ToList();
    }
}