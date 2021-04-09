using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Routes.Clothes;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Transfers;
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
            var request = RestRequest.GetRequest(ControllerName);

            Assert.True($"api/{ControllerName}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest_AdditionalRoute()
        {
            const string additionalRoute = "additionalRoute";
            var request = RestRequest.GetRequest(ControllerName, additionalRoute);

            Assert.True($"api/{ControllerName}/{additionalRoute}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest_Parameters()
        {
            var parameters = new List<string>
            {
                "first",
                "second",
            };
            var request = RestRequest.GetRequest(ControllerName, parameters);

            Assert.True($"api/{ControllerName}/{parameters.First()}/{parameters.Last()}".
                        Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest_AdditionalRoute_Empty()
        {
            string additionalRoute = String.Empty;
            var request = RestRequest.GetRequest(ControllerName, additionalRoute);

            Assert.True($"api/{ControllerName}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения по идентификатору
        /// </summary>
        [Fact]
        public void GetByIdJsonRequest()
        {
            const int id = 123;
            const string additionalRoute = "additionalRoute";
            var request = RestRequest.GetRequest(id, ControllerName, additionalRoute);

            Assert.True($"api/{ControllerName}/{additionalRoute}/{id}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения по идентификатору
        /// </summary>
        [Fact]
        public void GetByIdAdditionalJsonRequest()
        {
            const int id = 123;
            var request = RestRequest.GetRequest(id, ControllerName);

            Assert.True($"api/{ControllerName}/{id}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        [Fact]
        public void PostJsonRequest()
        {
            var request = RestRequest.PostRequest(ControllerName);

            Assert.True($"api/{ControllerName}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на отправку коллекции данных
        /// </summary>
        [Fact]
        public void PostCollectionJsonRequest()
        {
            var request = RestRequest.PostRequestCollection( ControllerName);

            Assert.True($"api/{ControllerName}/{BaseRoutes.POST_COLLECTION_ROUTE}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Имя контроллера
        /// </summary>
        private static string ControllerName => "TestController";
    }
}