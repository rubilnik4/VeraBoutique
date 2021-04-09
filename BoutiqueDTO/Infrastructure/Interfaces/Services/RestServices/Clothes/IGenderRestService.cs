using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис типа пола
    /// </summary>
    public interface IGenderRestService : IRestServiceBase<GenderType, IGenderDomain>
    {
        /// <summary>
        /// Получить данные типа пола с категорией асинхронно
        /// </summary>
        public Task<IResultCollection<IGenderCategoryDomain>> GetGenderCategories();
    }
}