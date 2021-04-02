using System.IO;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesDetailViewModelItems;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Xamarin.Forms;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    public class ClothesDetailViewModel : NavigationBaseViewModel<ClothesDetailNavigationParameters>
    {
        public ClothesDetailViewModel(IClothesRestService clothesRestService)
        {
            _clothesRestService = clothesRestService;
        }

        /// <summary>
        /// Получить данные одежды
        /// </summary>
        private readonly IClothesRestService _clothesRestService;

        /// <summary>
        /// Одежда. Детальная информация
        /// </summary>
        private ClothesDetailViewModelItem? _clothesDetailViewModelItem;

        /// <summary>
        /// Одежда. Детальная информация
        /// </summary>
        public ClothesDetailViewModelItem? ClothesDetailViewModelItem
        {
            get => _clothesDetailViewModelItem;
            private set => SetProperty(ref _clothesDetailViewModelItem, value);
        }

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected override async Task<IResultError> InitializeAction(ClothesDetailNavigationParameters clothesDetailNavigationParameters) =>
            await _clothesRestService.GetAsync(clothesDetailNavigationParameters.ClothesId).
            ResultValueVoidOkTaskAsync(clothes => ClothesDetailViewModelItem = new ClothesDetailViewModelItem(clothes));
    }
}