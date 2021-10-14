using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Routes.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Base
{
    /// <summary>
    /// Запросы к серверу. Тесты
    /// </summary>
    public class RestRequestTest
    {
        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest()
        {
            var request = RestRequest.GetRequest(ControllerName);

            Assert.True($"api/Test".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest_AdditionalRoute()
        {
            const string additionalRoute = "additionalRoute";
            var request = RestRequest.GetRequest(ControllerName, additionalRoute);

            Assert.True($"api/Test/{additionalRoute}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
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

            Assert.True($"api/Test/{parameters.First()}/{parameters.Last()}".
                        Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения
        /// </summary>
        [Fact]
        public void GetJsonRequest_Additional_Parameters()
        {
            var parameters = new List<string>
            {
                "first",
                "second",
            };
            const string additionalRoute = "additionalRoute";
            var request = RestRequest.GetRequest(ControllerName, additionalRoute, parameters);

            Assert.True($"api/Test/{additionalRoute}/{parameters.First()}/{parameters.Last()}".
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

            Assert.True($"api/Test".Equals(request, StringComparison.InvariantCultureIgnoreCase));
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

            Assert.True($"api/Test/{additionalRoute}/{id}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос получения по идентификатору
        /// </summary>
        [Fact]
        public void GetByIdAdditionalJsonRequest()
        {
            const int id = 123;
            var request = RestRequest.GetRequest(id, ControllerName);

            Assert.True($"api/Test/{id}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на отправку данных
        /// </summary>
        [Fact]
        public void PostJsonRequest()
        {
            var request = RestRequest.PostRequest(ControllerName);

            Assert.True($"api/Test".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Запрос на отправку коллекции данных
        /// </summary>
        [Fact]
        public void PostCollectionJsonRequest()
        {
            var request = RestRequest.PostRequestCollection( ControllerName);

            Assert.True($"api/Test/{BaseRoutes.POST_COLLECTION_ROUTE}".Equals(request, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Имя контроллера
        /// </summary>
        private static string ControllerName => "TestController";
    }
}