using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTO.Models.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Authorize
{
    public class ProfileRestServiceTest
    {
        /// <summary>
        /// Получить личную информацию
        /// </summary>
        [Fact]
        public async Task GetProfile_Ok()
        {
            var user = IdentityData.BoutiqueUsers.First();
            var userTransfer = IdentityTransfersData.BoutiqueUserTransfers.First();
            var userResult = userTransfer.ToResultValue();
            var restHttpClient = RestClientMock.GetRestClient(userResult);
            var boutiqueUserTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var authorizeRestService = new ProfileRestService(restHttpClient.Object, boutiqueUserTransferConverter);

            var resultProfile = await authorizeRestService.GetProfile();

            Assert.True(resultProfile.OkStatus);
            Assert.True(user.Equals(resultProfile.Value));
        }

        /// <summary>
        /// Получить личную информацию
        /// </summary>
        [Fact]
        public async Task GetProfile_Error()
        {
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var userResult = error.ToResultValue<BoutiqueUserTransfer>();
            var restHttpClient = RestClientMock.GetRestClient(userResult);
            var boutiqueUserTransferConverter = BoutiqueUserTransferConverterMock.BoutiqueUserTransferConverter;
            var authorizeRestService = new ProfileRestService(restHttpClient.Object, boutiqueUserTransferConverter);

            var resultProfile = await authorizeRestService.GetProfile();

            Assert.True(resultProfile.HasErrors);
            Assert.IsType<RestMessageErrorResult>(resultProfile.Errors.First());
        }
    }
}