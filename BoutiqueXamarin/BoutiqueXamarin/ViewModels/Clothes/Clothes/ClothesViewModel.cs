using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.Models.Interfaces.Result;
using Prism.Common;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public class ClothesViewModel: NavigationBaseViewModel<ClothesNavigationParameters>
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
        public IReadOnlyCollection<IClothesDomain> Clothes { get; private set; } = 
            new List<IClothesDomain>();

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected override async Task<IResultError> InitializeAction(ClothesNavigationParameters clothesParameters) =>
            await _clothesRestService.GetClothesAsync(clothesParameters.GenderType, clothesParameters.ClothesType).
            ResultCollectionVoidOkTaskAsync(clothes => Clothes = clothes);
    }
}