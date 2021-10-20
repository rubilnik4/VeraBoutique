using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Base;
using BoutiqueXamarin.Models.Implementations.Navigation.Clothes;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation.Clothes
{
    /// <summary>
    /// Сервис навигации к странице одежды
    /// </summary>
    public interface IClothesNavigationService : IBaseNavigationService<ClothesNavigationParameters>
    {
        /// <summary>
        /// Перейти к странице
        /// </summary>
        Task<INavigationResult> NavigateTo(GenderType genderType, IClothesTypeDomain clothesTypeDomain);
    }
}