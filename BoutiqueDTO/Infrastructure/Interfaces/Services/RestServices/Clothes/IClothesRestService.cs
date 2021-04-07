using System.IO;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    public interface IClothesRestService : IRestServiceBase<int, IClothesMainDomain>
    {
        /// <summary>
        /// Получить данные одежды
        /// </summary>
        Task<IResultCollection<IClothesDomain>> GetClothesAsync(GenderType genderType, string clothesType);

        /// <summary>
        /// Получить изображение одежды
        /// </summary>
        Task<IResultValue<byte[]>> GetImageAsync(int clothesId);
    }
}