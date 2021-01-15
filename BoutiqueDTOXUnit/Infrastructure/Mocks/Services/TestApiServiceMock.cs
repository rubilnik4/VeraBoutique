using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

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
        public static Mock<ITestApiService> GetTestApiServicePost(IResultCollection<TestEnum> resultPost) =>
              new Mock<ITestApiService>().
            Void(mock => mock.Setup(service => service.PostCollection(It.IsAny<IEnumerable<TestTransfer>>())).
                              ReturnsAsync(resultPost));

        /// <summary>
        /// Получить тестовый Api сервис
        /// </summary>
        public static Mock<ITestApiService> GetTestApiServiceDelete(IResultError resultDelete) =>
            new Mock<ITestApiService>().
            Void(mock => mock.Setup(service => service.Delete()).
                              ReturnsAsync(resultDelete));
    }
}