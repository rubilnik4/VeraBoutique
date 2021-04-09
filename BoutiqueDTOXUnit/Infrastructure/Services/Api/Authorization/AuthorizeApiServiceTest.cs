using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.Api.Authorization
{
    public class AuthorizeApiServiceTest
    {
        /// <summary>
        /// Авторизация через токен
        /// </summary>
        [Fact]
        public async Task Authorize_Ok()
        {
            var authorizeTransfer = AuthorizeTransfersData.AuthorizeTransfers.First();
            const string jwtToken =  "jwtToken";
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, jwtToken);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var authorizeApiService = new AuthorizeApiService(restClient.Object);

            var result = await authorizeApiService.AuthorizeJwt(authorizeTransfer);

            Assert.True(result.OkStatus);
            Assert.True(jwtToken == result.Value);
        }

        /// <summary>
        /// Получение данных. Ошибка
        /// </summary>
        [Fact]
        public async Task Authorize_Error()
        {
            var authorizeTransfer = AuthorizeTransfersData.AuthorizeTransfers.First();
            const string jwtToken = "jwtToken";
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, jwtToken);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var authorizeApiService = new AuthorizeApiService(restClient.Object);

            var result = await authorizeApiService.AuthorizeJwt(authorizeTransfer);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }
    }
}