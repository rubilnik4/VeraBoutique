using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.Api;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api;
using Functional.FunctionalExtensions.Sync;
using Moq;
using RestSharp;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.Api.Base
{
    /// <summary>
    /// Базовый сервис работы с api. Тесты
    /// </summary>
    public class ApiServiceTest
    {
        [Fact]
        public async Task Get_Ok()
        {
            var testTransfers = TestTransferData.TestTransfers.ToList();
            
            var restRequest = GetRestResponse(HttpStatusCode.OK, testTransfers);
            var restClient = GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = await testApiService.Get();
           
            Assert.True(result.OkStatus);
            Assert.True(testTransfers.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Получить ответ сервера
        /// </summary>
        private static IRestResponse<TValue> GetRestResponse<TValue>(HttpStatusCode httpStatusCode, TValue value)
            where TValue : notnull =>
            new RestResponse<TValue>()
            {
                StatusCode = httpStatusCode,
                Data = value,
            };

        /// <summary>
        /// Клиент для Api сервисов
        /// </summary>
        private static Mock<IRestClient> GetRestClient<TValue>(IRestResponse<TValue> restResponse) 
            where TValue : notnull =>
            new Mock<IRestClient>().
            Void(mock => mock.Setup(client => client.ExecuteAsync<TValue>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).
                              ReturnsAsync(restResponse));
    }
}