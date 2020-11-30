using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueMVCXUnit.Data.Database.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueMVCXUnit.Controllers.Base.Mocks
{
    /// <summary>
    /// Тестовые сервис удаления данных
    /// </summary>
    public class DatabaseServiceDeleteMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, DeleteOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> deleteFunc) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.Get()).ReturnsAsync(testDomains)).
            Void(serviceMock => serviceMock.Setup(service => service.Delete(It.IsAny<TestEnum>())).ReturnsAsync(deleteFunc));

        /// <summary>
        /// Функция удаления по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> DeleteOkFunc(IResultCollection<ITestDomain> testDomains) =>
            id => testDomains.ResultValueOk(tests => SearchInDomains.FirstDomain(tests, id));

        /// <summary>
        /// Функция удаления по идентификатору.Элемент не найден
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> DeleteNotFoundFunc() =>
            id => new ResultValue<ITestDomain>(ErrorData.NotFoundError);

    }
}