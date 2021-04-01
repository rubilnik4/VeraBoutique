using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesViewModelItems;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Prism.Common;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public class ClothesViewModel : NavigationBaseViewModel<ClothesNavigationParameters>
    {
        public ClothesViewModel(IClothesRestService clothesRestService)
        {
            _clothesRestService = clothesRestService;
        }

        /// <summary>
        /// Получить данные одежды
        /// </summary>
        private readonly IClothesRestService _clothesRestService;

        /// <summary>
        /// Одежда
        /// </summary>
        private IReadOnlyCollection<ClothesViewModelColumnItem> _clothesViewModelColumnItems =
            new List<ClothesViewModelColumnItem>();

        /// <summary>
        /// Одежда
        /// </summary>
        public IReadOnlyCollection<ClothesViewModelColumnItem> ClothesViewModelColumnItems
        {
            get => _clothesViewModelColumnItems;
            set => SetProperty(ref _clothesViewModelColumnItems, value);
        }

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected override async Task<IResultError> InitializeAction(ClothesNavigationParameters clothesParameters) =>
            await _clothesRestService.GetClothesAsync(clothesParameters.GenderType, clothesParameters.ClothesType).
            ResultCollectionVoidOkTaskAsync(clothes => ClothesViewModelColumnItems = GetClothesItems(clothes));

        /// <summary>
        /// Преобразовать в модели одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesViewModelColumnItem> GetClothesItems(IEnumerable<IClothesDomain> clothesDomains) =>
            clothesDomains.
            Select(clothesDomain => new ClothesViewModelItem(clothesDomain)).
            ToList().
            Map(clothesItems => (clothesItems.Where((clothes, index) => index % 2 == 0),
                                 clothesItems.Where((clothes, index) => index % 2 != 0))).
            Map(clothesPair => clothesPair.Item1.ZipLong(clothesPair.Item2,
                                                         (first, second) => new ClothesViewModelColumnItem(first, second))).
            ToList();
    }
}