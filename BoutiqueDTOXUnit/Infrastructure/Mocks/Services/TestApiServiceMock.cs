using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit.Sdk;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Services
{
    /// <summary>
    /// Тестовый Api сервис
    /// </summary>
    public static class TestApiServiceMock
    {
        /// <summary>
        /// Получить тестовый Api сервис
        /// </summary>
        public static Mock<ITestApiService> GetTestApiServiceGet(IResultCollection<TestTransfer> resultGet) =>
            new Mock<ITestApiService>().
            Void(mock => mock.Setup(service => service.Get()).
                              Returns(resultGet)).
            Void(mock => mock.Setup(service => service.GetAsync()).
                              ReturnsAsync(resultGet));

        /// <summary>
        /// Получить тестовый Api сервис
        /// </summary>
        public static Mock<ITestApiService> GetTestApiServiceGetId(IResultValue<TestTransfer> resultGet) =>
            new Mock<ITestApiService>().
            Void(mock => mock.Setup(service => service.Get(It.IsAny<TestEnum>())).
                              Returns(resultGet)).
            Void(mock => mock.Setup(service => service.GetAsync(It.IsAny<TestEnum>())).
                              ReturnsAsync(resultGet));

        /// <summary>
        /// Получить тестовый Api сервис
        /// </summary>
        public static Mock<ITestApiService> GetTestApiServicePost(IResultCollection<TestEnum> resultPost) =>
            new Mock<ITestApiService>().
            Void(mock => mock.Setup(service => service.PostCollection(It.IsAny<IEnumerable<TestTransfer>>())).
                              Returns(resultPost)).
            Void(mock => mock.Setup(service => service.PostCollectionAsync(It.IsAny<IEnumerable<TestTransfer>>())).
                              ReturnsAsync(resultPost));

        /// <summary>
        /// Получить тестовый Api сервис
        /// </summary>
        public static Mock<ITestApiService> GetTestApiServiceDelete(IResultError resultDelete) =>
            new Mock<ITestApiService>().
            Void(mock => mock.Setup(service => service.Delete()).
                              Returns(resultDelete)).
            Void(mock => mock.Setup(service => service.DeleteAsync()).
                              ReturnsAsync(resultDelete));
    }
}