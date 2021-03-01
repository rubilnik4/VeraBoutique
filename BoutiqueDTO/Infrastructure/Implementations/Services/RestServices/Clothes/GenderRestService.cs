using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис типа пола в базу данных
    /// </summary>
    public class GenderRestService : RestServiceBase<GenderType, IGenderDomain, GenderTransfer>, IGenderRestService
    {
        public GenderRestService(IGenderApiService genderApiService, 
                                 IGenderTransferConverter genderTransferConverter,
                                 IGenderCategoryTransferConverter genderCategoryTransferConverter)
            :base(genderApiService, genderTransferConverter)
        {
            _genderApiService = genderApiService;
            _genderCategoryTransferConverter = genderCategoryTransferConverter;
        }

        /// <summary>
        /// Api сервис типа пола
        /// </summary>
        private readonly IGenderApiService _genderApiService;

        /// <summary>
        /// Конвертер типа пола c категорией в трансферную модель
        /// </summary>
        private readonly IGenderCategoryTransferConverter _genderCategoryTransferConverter;

        /// <summary>
        /// Получить данные типа пола с категорией
        /// </summary>
        public IResultCollection<IGenderCategoryDomain> GetGenderCategories() =>
            new ResultValue<IGenderApiService>(_genderApiService).
            ResultValueBindOkToCollection(api => api.GetGenderCategories()).
            ResultCollectionBindOk(transfers => _genderCategoryTransferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить данные типа пола с категорией
        /// </summary>
        public async Task<IResultCollection<IGenderCategoryDomain>> GetGenderCategoriesAsync() =>
            await new ResultValue<IGenderApiService>(_genderApiService).
            ResultValueBindOkToCollectionAsync(api => api.GetGenderCategoriesAsync()).
            ResultCollectionBindOkTaskAsync(transfers => _genderCategoryTransferConverter.FromTransfers(transfers));
    }
}