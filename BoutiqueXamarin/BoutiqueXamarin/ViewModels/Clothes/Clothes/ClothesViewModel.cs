using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueXamarin.ViewModels.Base;
using Functional.Models.Interfaces.Result;
using Prism.Navigation;

namespace BoutiqueXamarin.ViewModels.Clothes.Clothes
{
    /// <summary>
    /// Списки одежды
    /// </summary>
    public class ClothesViewModel: ViewModelBase
    {
        public ClothesViewModel(INavigationService navigationService, IClothesApiService clothesApiService)
            :base(navigationService)
        {
            _clothesApiService = clothesApiService;
        }

        /// <summary>
        /// Api сервис одежды
        /// </summary>
        private readonly IClothesApiService _clothesApiService;

        /// <summary>
        /// Асинхронная загрузка параметров модели
        /// </summary>
        protected override Task<IResultError> InitializeAction(INavigationParameters parameters) =>;

        private static async Task<IResultCollection<IClothesDomain>> GetClothes(IClothesApiService clothesApiService,
                                                                                GenderType genderType, string clothesType) =>
            await clothesApiService.GetClothes(genderType, clothesType);
    }
}