using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes
{
    /// <summary>
    /// Сервис навигации к странице детализации одежды
    /// </summary>
    public interface IClothesDetailNavigationService : IBaseNavigationService<ClothesDetailNavigationParameters>
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task NavigateTo(IClothesDetailDomain clothesDetail);
    }
}