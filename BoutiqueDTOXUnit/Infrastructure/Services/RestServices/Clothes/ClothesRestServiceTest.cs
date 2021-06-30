using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис одежды. Тесты
    /// </summary>
    public class ClothesRestServiceTest
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothes_Ok()
        {
            var clothes = ClothesTransfersData.ClothesTransfers;
            var resultClothes = new ResultCollection<ClothesTransfer>(clothes);
            var restClient = RestClientMock.GetRestClient(resultClothes);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;
            var clothesRestService = GetClothesRestService(restClient.Object);

            var result = await clothesRestService.GetClothes(clothes.First().GenderType, clothes.First().ClothesTypeName);
            var clothesDomains = clothesTransferConverter.FromTransfers(clothes);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(clothesDomains.Value));
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothes_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultClothes = new ResultCollection<ClothesTransfer>(error);
            var restClient = RestClientMock.GetRestClient(resultClothes);
            var clothesRestService = GetClothesRestService(restClient.Object);

            var result = await clothesRestService.GetClothes(GenderType.Male, String.Empty);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothesDetail_Ok()
        {
            var clothesDetails = ClothesTransfersData.ClothesDetailTransfers;
            var resultClothes = new ResultCollection<ClothesDetailTransfer>(clothesDetails);
            var restClient = RestClientMock.GetRestClient(resultClothes);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesDetailTransferConverter;
            var clothesRestService = GetClothesRestService(restClient.Object);

            var result = await clothesRestService.GetClothesDetails(clothesDetails.First().GenderType, clothesDetails.First().ClothesTypeName);
            var clothesDomains = clothesTransferConverter.FromTransfers(clothesDetails);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(clothesDomains.Value));
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothesDetail_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultClothes = new ResultCollection<ClothesDetailTransfer>(error);
            var restClient = RestClientMock.GetRestClient(resultClothes);
            var clothesRestService = GetClothesRestService(restClient.Object);

            var result = await clothesRestService.GetClothesDetails(GenderType.Male, String.Empty);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetClothesImage_Ok()
        {
            var clothes = ClothesTransfersData.ClothesMainTransfers.First();
            var resultClothes = new ResultValue<byte[]>(clothes.Image);
            var restClient = RestClientMock.GetRestClient(resultClothes);
            var clothesRestService = GetClothesRestService(restClient.Object);

            var result = await clothesRestService.GetImage(clothes.Id);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(clothes.Image));
        }

        /// <summary>
        /// Получение изображения
        /// </summary>
        [Fact]
        public async Task GetClothesImage_Error()
        {
            var clothes = ClothesTransfersData.ClothesMainTransfers.First();
            var error = ErrorTransferData.ErrorBadRequest;
            var resultClothes = new ResultValue<byte[]>(error);
            var restClient = RestClientMock.GetRestClient(resultClothes);
            var clothesRestService = GetClothesRestService(restClient.Object);

            var result = await clothesRestService.GetImage(clothes.Id);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Получить сервис
        /// </summary>
        private static IClothesRestService GetClothesRestService(IRestHttpClient restClient) =>
            new ClothesRestService(restClient, ClothesTransferConverterMock.ClothesTransferConverter,
                                   ClothesTransferConverterMock.ClothesDetailTransferConverter,
                                   ClothesTransferConverterMock.ClothesMainTransferConverter);
    }
}