using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Clothes;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.Api;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.Api.Clothes
{
    /// <summary>
    /// Api сервис типа пола. Тесты
    /// </summary>
    public class GenderApiServiceTest
    {
        /// <summary>
        /// Получить данные типа пола с категорией
        /// </summary>
        [Fact]
        public void GetGenderCategories_Ok()
        {
            var genders = GenderTransfersData.GenderCategoryTransfers.ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, genders);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var genderApiService = new GenderApiService(restClient.Object);

            var result = genderApiService.GetGenderCategories();

            Assert.True(result.OkStatus);
            Assert.True(genders.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Получить данные типа пола с категорией. Ошибка
        /// </summary>
        [Fact]
        public void GetGenderCategories_Error()
        {
            var genders = GenderTransfersData.GenderCategoryTransfers.ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, genders);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var genderApiService = new GenderApiService(restClient.Object);

            var result = genderApiService.GetGenderCategories();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Получить данные типа пола с категорией
        /// </summary>
        [Fact]
        public async Task GetGenderCategoriesAsync_Ok()
        {
            var genders = GenderTransfersData.GenderCategoryTransfers.ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, genders);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var genderApiService = new GenderApiService(restClient.Object);

            var result = await genderApiService.GetGenderCategoriesAsync();

            Assert.True(result.OkStatus);
            Assert.True(genders.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Получить данные типа пола с категорией. Ошибка
        /// </summary>
        [Fact]
        public async Task GetGenderCategoriesAsync_Error()
        {
            var genders = GenderTransfersData.GenderCategoryTransfers.ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, genders);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var genderApiService = new GenderApiService(restClient.Object);

            var result = await genderApiService.GetGenderCategoriesAsync();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }
    }
}