using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    public interface IClothesRestService : IRestServiceBase<int, IClothesMainDomain>
    {
        /// <summary>
        /// Получить данные одежды
        /// </summary>
        Task<IResultCollection<IClothesDomain>> GetClothes(GenderType genderType, string clothesType);

        /// <summary>
        /// Получить уточненные данные одежды
        /// </summary>
        Task<IResultCollection<IClothesDetailDomain>> GetClothesDetails(GenderType genderType, string clothesType);

        /// <summary>
        /// Получить главное изображение одежды
        /// </summary>
        Task<IResultValue<byte[]>> GetImage(int clothesId);

        /// <summary>
        /// Получить изображения одежды
        /// </summary>
        Task<IResultCollection<IClothesImageDomain>> GetImages(int clothesId);
    }
}