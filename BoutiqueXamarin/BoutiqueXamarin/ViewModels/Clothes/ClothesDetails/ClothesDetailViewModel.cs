using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using BoutiqueXamarin.ViewModels.Clothes.Clothes.ClothesDetailViewModelItems;

namespace BoutiqueXamarin.ViewModels.Clothes.ClothesDetails
{
    public class ClothesDetailViewModel : NavigationBaseViewModel<ClothesDetailNavigationParameters, IClothesDetailNavigationService>
    {
        public ClothesDetailViewModel(IClothesRestService clothesRestService,
                                      IClothesDetailNavigationService clothesDetailNavigationService)
            :base(clothesDetailNavigationService)
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
            private set => _clothesDetailViewModelItem= value;
        }

        ///// <summary>
        ///// Асинхронная загрузка параметров модели
        ///// </summary>
        //protected override async Task<IResultError> InitializeAction(ClothesDetailNavigationParameters clothesDetailNavigationParameters) =>
        //    await _clothesRestService.GetAsync(clothesDetailNavigationParameters.ClothesId).
        //    ResultValueVoidOkTaskAsync(clothes => ClothesDetailViewModelItem = new ClothesDetailViewModelItem(clothes));
    }
}