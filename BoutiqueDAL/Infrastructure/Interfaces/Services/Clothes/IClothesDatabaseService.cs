using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис информации об одежде в базе данных
    /// </summary>
    public interface IClothesDatabaseService : IDatabaseService<int, IClothesInformationDomain>
    {
        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        Task<IResultCollection<IClothesShortDomain>> GetWithoutImages();

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        Task<IResultValue<IClothesInformationDomain>> GetIncludesById(int id);
    }
}