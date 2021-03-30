using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис одежды. Тесты
    /// </summary>
    public class ClothesApiServiceTest
    {
        /// <summary>
        /// Получить данные одежды
        /// </summary>
        [Fact]
        public async Task GetClothesAsync_Ok()
        {
            var clothesInitial = ClothesTransfersData.ClothesTransfers.First();
            var genderType = clothesInitial.GenderType;
            var clothesType = clothesInitial.ClothesTypeName;
            var clothes = ClothesTransfersData.ClothesTransfers.
                          Where(transfer => transfer.GenderType == genderType &&
                                            transfer.ClothesTypeName == clothesType).
                          ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, clothes);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var clothesApiService = new ClothesApiService(restClient.Object);

            var result = await clothesApiService.GetClothes(genderType, clothesType);

            Assert.True(result.OkStatus);
            Assert.True(clothes.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Получить данные одежды. Ошибка
        /// </summary>
        [Fact]
        public async Task GetClothesAsync_Error()
        {
            var clothesInitial = ClothesTransfersData.ClothesTransfers.First();
            var genderType = clothesInitial.GenderType;
            var clothesType = clothesInitial.ClothesTypeName;
            var clothes = ClothesTransfersData.ClothesTransfers.
                          Where(transfer => transfer.GenderType == genderType &&
                                            transfer.ClothesTypeName == clothesType).
                          ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, clothes);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var clothesApiService = new ClothesApiService(restClient.Object);

            var result = await clothesApiService.GetClothes(genderType, clothesType);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }
    }
}