using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис одежды
    /// </summary>
    public class ClothesRestService : RestServiceBase<int, IClothesMainDomain, ClothesMainTransfer>, IClothesRestService
    {
        public ClothesRestService(IClothesApiService clothesApiService,
                                  IClothesTransferConverter clothesTransferConverter,
                                  IClothesMainTransferConverter clothesMainTransferConverter)
            : base(clothesApiService, clothesMainTransferConverter)
        {
            _clothesApiService = clothesApiService;
            _clothesTransferConverter = clothesTransferConverter;
        }

        /// <summary>
        /// Api сервис одежды
        /// </summary>
        private readonly IClothesApiService _clothesApiService;

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        private readonly IClothesTransferConverter _clothesTransferConverter;

        /// <summary>
        /// Получить данные одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDomain>> GetClothesAsync(GenderType genderType, string clothesType) =>
            await new ResultValue<IClothesApiService>(_clothesApiService).
            ResultValueBindOkToCollectionAsync(api => api.GetClothes(genderType, clothesType)).
            ResultCollectionBindOkTaskAsync(transfers => _clothesTransferConverter.FromTransfers(transfers));
    }
}