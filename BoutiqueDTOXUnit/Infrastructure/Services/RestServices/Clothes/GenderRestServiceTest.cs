using System.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.GenderTransfers;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.RestService;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис типа пола. Тесты
    /// </summary>
    public class GenderRestServiceTest
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetGenderCategories_Ok()
        {
            var genders = GenderTransfersData.GenderCategoryTransfers;
            var resultGenders = new ResultCollection<GenderCategoryTransfer>(genders);
            var genderApiServiceGet = GenderApiServiceMock.GetGenderApiService(resultGenders);
            var genderTransferConverter = GenderTransferConverterMock.GenderTransferConverter;
            var genderCategoryTransferConverter = GenderTransferConverterMock.GenderCategoryTransferConverter;
            var genderRestService = new GenderRestService(genderApiServiceGet.Object, genderTransferConverter,
                                                        genderCategoryTransferConverter);

            var result = await genderRestService.GetGenderCategories();
            var genderDomains = genderCategoryTransferConverter.FromTransfers(genders);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(genderDomains.Value));
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetGenderCategories_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultGenders = new ResultCollection<GenderCategoryTransfer>(error);
            var genderApiServiceGet = GenderApiServiceMock.GetGenderApiService(resultGenders);
            var genderTransferConverter = GenderTransferConverterMock.GenderTransferConverter;
            var genderCategoryTransferConverter = GenderTransferConverterMock.GenderCategoryTransferConverter;
            var genderRestService = new GenderRestService(genderApiServiceGet.Object, genderTransferConverter,
                                                        genderCategoryTransferConverter);

            var result = await genderRestService.GetGenderCategories();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }
    }
}