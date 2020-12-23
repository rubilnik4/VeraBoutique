using System;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using RestSharp;
using Xunit;
using Xunit.Sdk;

namespace BoutiqueDTOXUnit.Infrastructure.Services.Api.Base
{
    /// <summary>
    /// Запросы к серверу. Тесты
    /// </summary>
    public class ApiRestRequestTest
    {
        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest()
        {
            var request = ApiRestRequest.GetJsonRequest(ControllerName);

            Assert.Equal(Method.GET, request.Method);
            Assert.True("api/Test".Equals(request.Resource, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения по идентификатору
        /// </summary>
        [Fact]
        public void GetByIdJsonRequest()
        {
            const int id = 123;
            var request = ApiRestRequest.GetJsonRequest(id, ControllerName);

            Assert.Equal(Method.GET, request.Method);
            Assert.True("api/Test/id".Equals(request.Resource, StringComparison.InvariantCultureIgnoreCase));
            Assert.Equal(id.ToString(), request.Parameters.First().Value!);
        }

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        [Fact]
        public void PostJsonRequest()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var request = ApiRestRequest.PostJsonRequest<TestEnum, TestTransfer>(testTransfer, ControllerName);

            Assert.Equal(Method.POST, request.Method);
            Assert.True("api/Test".Equals(request.Resource, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на отправку коллекции данных
        /// </summary>
        [Fact]
        public void PostCollectionJsonRequest()
        {
            var testTransfers = TestTransferData.TestTransfers;
            var request = ApiRestRequest.PostJsonRequest<TestEnum, TestTransfer>(testTransfers, ControllerName);

            Assert.Equal(Method.POST, request.Method);
            Assert.True("api/Test/collection".Equals(request.Resource, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на изменение данных
        /// </summary>
        [Fact]
        public void PutJsonRequest()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var request = ApiRestRequest.PutJsonRequest<TestEnum, TestTransfer>(testTransfer, ControllerName);

            Assert.Equal(Method.PUT, request.Method);
            Assert.True("api/Test".Equals(request.Resource, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на удаление данных
        /// </summary>
        [Fact]
        public void DeleteJsonRequest()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var request = ApiRestRequest.DeleteJsonRequest(testTransfer.Id, ControllerName);

            Assert.Equal(Method.DELETE, request.Method);
            Assert.True("api/Test/id".Equals(request.Resource, StringComparison.InvariantCultureIgnoreCase));
            Assert.Equal(testTransfer.Id.ToString(), request.Parameters.First().Value!);
        }

        /// <summary>
        /// Имя контроллера
        /// </summary>
        private static string ControllerName => "TestController";
    }
}