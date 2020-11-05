using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис информации об одежде в базе данных
    /// </summary>
    public interface IClothesDatabaseService : IDatabaseService<int, IClothesDomain>
    {
        /// <summary>
        /// Получить одежду без изображений по типу полу и типу одежды
        /// </summary>
        Task<IResultCollection<IClothesShortDomain>> GetClothesShorts(GenderType genderType, string clothesType);

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        Task<IResultValue<IClothesDomain>> GetIncludesById(int id);
    }
}