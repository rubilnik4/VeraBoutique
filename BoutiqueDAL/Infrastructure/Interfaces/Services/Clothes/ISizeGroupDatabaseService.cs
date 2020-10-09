using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис группы размеров одежды в базе данных
    /// </summary>
    public interface ISizeGroupDatabaseService : IDatabaseService<(ClothesSizeType, int), ISizeGroupDomain>
    {
        /// <summary>
        /// Получить группу размеров совместно со списком размеров
        /// </summary>
        Task<IResultValue<ISizeGroupDomain>> GetSizeGroupIncludeSize(ClothesSizeType clothesSizeType,
                                                                     int sizeNormalize);
    }
}