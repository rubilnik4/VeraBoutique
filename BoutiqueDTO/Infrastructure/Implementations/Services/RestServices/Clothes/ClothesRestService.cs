using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueDTO.Routes.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис одежды
    /// </summary>
    public class ClothesRestService : RestServiceBase<int, IClothesMainDomain, ClothesMainTransfer>, IClothesRestService
    {
        public ClothesRestService(IRestHttpClient restHttpClient,
                                  IClothesTransferConverter clothesTransferConverter,
                                  IClothesDetailTransferConverter clothesDetailTransferConverter,
                                  IClothesMainTransferConverter clothesMainTransferConverter,
                                  IClothesImageTransferConverter clothesImageTransferConverter)
            : base(restHttpClient, clothesMainTransferConverter)
        {
            _clothesTransferConverter = clothesTransferConverter;
            _clothesDetailTransferConverter = clothesDetailTransferConverter;
            _clothesImageTransferConverter = clothesImageTransferConverter;
        }

        /// <summary>
        /// Конвертер одежды в трансферную модель
        /// </summary>
        private readonly IClothesTransferConverter _clothesTransferConverter;

        /// <summary>
        /// Конвертер уточненной информации об одежде в трансферную модель
        /// </summary>
        private readonly IClothesDetailTransferConverter _clothesDetailTransferConverter;

        /// <summary>
        /// Конвертер изображений в трансферную модель
        /// </summary>
        private readonly IClothesImageTransferConverter _clothesImageTransferConverter;

        /// <summary>
        /// Получить данные одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDomain>> GetClothes(GenderType genderType, string clothesType) =>
            await new List<string> { genderType.ToString(), clothesType }.
            Map(parameters => RestRequest.GetRequest(ControllerName, parameters)).
            MapAsync(request => RestHttpClient.GetCollectionAsync<ClothesTransfer>(request)).
            ResultCollectionBindOkTaskAsync(transfers => _clothesTransferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить уточненные данные одежды
        /// </summary>
        public async Task<IResultCollection<IClothesDetailDomain>> GetClothesDetails(GenderType genderType, string clothesType) =>
            await new List<string> { genderType.ToString(), clothesType }.
            Map(parameters => RestRequest.GetRequest(ControllerName, ClothesRoutes.DETAIL_ROUTE, parameters)).
            MapAsync(request => RestHttpClient.GetCollectionAsync<ClothesDetailTransfer>(request)).
            ResultCollectionBindOkTaskAsync(transfers => _clothesDetailTransferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить главное изображение одежды
        /// </summary>
        public async Task<IResultValue<byte[]>> GetImage(int clothesId) =>
            await RestRequest.GetRequest(clothesId, ControllerName, ClothesRoutes.IMAGE_ROUTE).
            MapAsync(request => RestHttpClient.GetByteAsync(request));

        /// <summary>
        /// Получить изображения одежды
        /// </summary>
        public async Task<IResultCollection<IClothesImageDomain>> GetImages(int clothesId) =>
            await RestRequest.GetRequest(clothesId, ControllerName, ClothesRoutes.IMAGES_ROUTE).
            MapAsync(request => RestHttpClient.GetCollectionAsync<ClothesImageTransfer>(request)).
            ResultCollectionBindOkTaskAsync(transfers => _clothesImageTransferConverter.FromTransfers(transfers));
    }
}