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
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
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
        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public void Get_Ok()
        {
            var testTransfers = TestTransferData.TestTransfers.ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, testTransfers);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Get();

            Assert.True(result.OkStatus);
            Assert.True(testTransfers.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Получение данных. Ошибка
        /// </summary>
        [Fact]
        public void Get_Error()
        {
            var testTransfers = TestTransferData.TestTransfers.ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, testTransfers);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Get();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        [Fact]
        public void GetById_Ok()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, testTransfer);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Get(testTransfer.Id);

            Assert.True(result.OkStatus);
            Assert.True(testTransfer.Equals(result.Value));
        }

        /// <summary>
        /// Получение данных по идентификатору. Ошибка
        /// </summary>
        [Fact]
        public void GetById_Error()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, testTransfer);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Get(testTransfer.Id);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Отправка данных
        /// </summary>
        [Fact]
        public void Post_Ok()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testId = testTransfer.Id;
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, testId);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Post(testTransfer);

            Assert.True(result.OkStatus);
            Assert.True(testId.Equals(result.Value));
        }

        /// <summary>
        /// Отправка данных
        /// </summary>
        [Fact]
        public void Post_Error()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testId = testTransfer.Id;
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, testId);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Post(testTransfer);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Отправка данных
        /// </summary>
        [Fact]
        public void PostCollection_Ok()
        {
            var testTransfers = TestTransferData.TestTransfers;
            var testIds = testTransfers.Select(transfer => transfer.Id).ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.Created, testIds);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.PostCollection(testTransfers);

            Assert.True(result.OkStatus);
            Assert.True(testIds.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Отправка данных
        /// </summary>
        [Fact]
        public void PostCollection_Error()
        {
            var testTransfers = TestTransferData.TestTransfers;
            var testIds = testTransfers.Select(transfer => transfer.Id).ToList();
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, testIds);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.PostCollection(testTransfers);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        [Fact]
        public void Put_Ok()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testId = testTransfer.Id;
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.NoContent, testId);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Put(testTransfer);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Обновление данных. Ошибка
        /// </summary>
        [Fact]
        public void Put_Error()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testId = testTransfer.Id;
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, testId);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Put(testTransfer);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        [Fact]
        public void Delete_Ok()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testId = testTransfer.Id;
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.OK, testTransfer);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Delete(testId);

            Assert.True(result.OkStatus);
            Assert.True(testTransfer.Equals(result.Value));
        }

        /// <summary>
        /// Удаление данных. Ошибка
        /// </summary>
        [Fact]
        public void Delete_Error()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testId = testTransfer.Id;
            var restRequest = RestClientMock.GetRestResponse(HttpStatusCode.BadRequest, testTransfer);
            var restClient = RestClientMock.GetRestClient(restRequest);
            var testApiService = new TestApiService(restClient.Object);

            var result = testApiService.Delete(testId);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }
    }
}