using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис одежды в базе данных
    /// </summary>
    public interface IClothesShortDatabaseService : IDatabaseService<int, IClothesShortDomain>
    {
        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        Task<IResultCollection<IClothesShortDomain>> GetWithoutImages();
    }
}