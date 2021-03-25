using System.Net;
using System.Threading;
using Functional.FunctionalExtensions.Sync;
using Moq;
using RestSharp;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Services
{
    /// <summary>
    /// Клиент для Api сервисов
    /// </summary>
    public static class RestClientMock
    {
        /// <summary>
        /// Получить клиент для Api сервисов
        /// </summary>
        public static Mock<IRestClient> GetRestClient<TValue>(IRestResponse<TValue> restResponse)
            where TValue : notnull =>
            new Mock<IRestClient>().
            Void(mock => mock.Setup(client => client.Execute<TValue>(It.IsAny<IRestRequest>())).
                              Returns(restResponse)).
            Void(mock => mock.Setup(client => client.Execute(It.IsAny<IRestRequest>())).
                              Returns(restResponse)).
            Void(mock => mock.Setup(client => client.ExecuteAsync<TValue>(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).
                              ReturnsAsync(restResponse)).
            Void(mock => mock.Setup(client => client.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>())).
                              ReturnsAsync(restResponse));

        /// <summary>
        /// Получить ответ сервера
        /// </summary>
        public static IRestResponse<TValue> GetRestResponse<TValue>(HttpStatusCode httpStatusCode, TValue value)
            where TValue : notnull =>
            new RestResponse<TValue>()
            {
                StatusCode = httpStatusCode,
                Data = value,
            };
    }
}