using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueDTO.Routes.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис одежды
    /// </summary>
    public class ClothesRestService : RestServiceBase<int, IClothesMainDomain, ClothesMainTransfer>, IClothesRestService
    {
        public ClothesRestService(IRestHttpClient restHttpClient,
                                  IClothesTransferConverter clothesTransferConverter,
                                  IClothesMainTransferConverter clothesMainTransferConverter)
            : base(restHttpClient, clothesMainTransferConverter)
        {
            _clothesTransferConverter = clothesTransferConverter;
        }

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        private readonly IClothesTransferConverter _clothesTransferConverter;

        /// <summary>
        /// Получить данные одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDomain>> GetClothesAsync(GenderType genderType, string clothesType) =>
            await new List<string> { genderType.ToString(), clothesType }.
            Map(parameters => RestRequest.GetRequest(ControllerName, parameters)).
            MapAsync(request => RestHttpClient.GetCollectionAsync<ClothesTransfer>(request)).
            ResultCollectionBindOkTaskAsync(transfers => _clothesTransferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить изображение одежды
        /// </summary>
        public async Task<IResultValue<byte[]>> GetImageAsync(int clothesId) =>
           await RestRequest.GetRequest(clothesId, ControllerName, ClothesRoutes.IMAGE_ROUTE).
           MapAsync(request => RestHttpClient.GetByteAsync(request));
    }
}