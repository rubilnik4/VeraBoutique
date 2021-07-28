using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис информации об одежде в базе данных
    /// </summary>
    public interface IClothesDatabaseService : IDatabaseService<int, IClothesMainDomain>
    {
        /// <summary>
        /// Получить одежду без изображений по типу полу и типу одежды
        /// </summary>
        Task<IResultCollection<IClothesDomain>> GetClothes(GenderType genderType, string clothesType);

        /// <summary>
        /// Получить уточненную информацию об одежде по типу полу и типу одежды
        /// </summary>
        Task<IResultCollection<IClothesDetailDomain>> GetClothesDetails(GenderType genderType, string clothesType);

        /// <summary>
        /// Получить главное изображение одежды по идентификатору
        /// </summary>
        Task<IResultValue<byte[]>> GetImage(int id);

        /// <summary>
        /// Получить изображения одежды по идентификатору
        /// </summary>
        Task<IResultCollection<IClothesImageDomain>> GetImages(int id);
    }
}