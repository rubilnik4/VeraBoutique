using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using Prism.Navigation;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Navigation
{
    /// <summary>
    /// Навигация к странице одежды
    /// </summary>
    public interface IClothesNavigationService
    {
        /// <summary>
        /// Перейти к странице одежды
        /// </summary>
        Task<INavigationResult> ToClothesPage(GenderType genderType, IClothesTypeDomain clothesTypeDomain);

        /// <summary>
        /// Перейти к странице информации одежды
        /// </summary>
        Task<INavigationResult> ToClothesDetailPage(IClothesDetailDomain clothesDetail, SizeType defaultSizeType);

        /// <summary>
        /// Перейти к странице выбора одежды
        /// </summary>
        Task<INavigationResult> ToChoicePage();
    }
}