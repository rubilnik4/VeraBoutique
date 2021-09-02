using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

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