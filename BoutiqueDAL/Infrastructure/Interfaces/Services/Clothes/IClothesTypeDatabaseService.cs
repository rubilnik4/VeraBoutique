using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис вида одежды в базе данных
    /// </summary>
    public interface IClothesTypeDatabaseService : IDatabaseService<string, IClothesTypeDomain>
    {
        /// <summary>
        /// Получить вид одежды по типу пола
        /// </summary>
        Task<IResultCollection<IClothesTypeDomain>> GetByGender(GenderType genderType);
    }
}