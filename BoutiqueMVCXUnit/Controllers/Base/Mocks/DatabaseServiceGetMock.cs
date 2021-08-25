using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueMVCXUnit.Data.Database.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;
using Moq;

namespace BoutiqueMVCXUnit.Controllers.Base.Mocks
{
    /// <summary>
    /// Тестовые сервис получения данных
    /// </summary>
    public static class DatabaseServiceGetMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, GetByIdOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> getByIdFunc) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.Get()).ReturnsAsync(testDomains)).
            Void(serviceMock => serviceMock.Setup(service => service.Get(It.IsAny<TestEnum>())).ReturnsAsync(getByIdFunc));

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> GetByIdOkFunc(IResultCollection<ITestDomain> testDomains) =>
            id => testDomains.ResultValueOk(tests => SearchInDomains.FirstDomain(tests, id));

        /// <summary>
        /// Функция поиска по идентификатору. Элемент не найден
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> GetByIdNotFoundFunc() =>
            id => new ResultValue<ITestDomain>(ErrorData.NotFoundErrorType);
    }
}