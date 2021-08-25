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
    /// Тестовые сервис удаления данных
    /// </summary>
    public class DatabaseServiceDeleteMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, DeleteOkFunc(testDomains), DeleteAllOk);

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> deleteFunc) =>
            GetTestDatabaseTable(testDomains, deleteFunc, DeleteAllOk);

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      IResultError deleteAllResult) =>
            GetTestDatabaseTable(testDomains, DeleteOkFunc(testDomains), deleteAllResult);

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> deleteFunc,
                                                                      IResultError deleteAllResult) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.Get()).ReturnsAsync(testDomains)).
            Void(serviceMock => serviceMock.Setup(service => service.Delete(It.IsAny<TestEnum>())).ReturnsAsync(deleteFunc)).
            Void(serviceMock => serviceMock.Setup(service => service.Delete()).ReturnsAsync(deleteAllResult));

        /// <summary>
        /// Удаление всех элементов
        /// </summary>
        public static IResultError DeleteAllOk =>
            new ResultError();

        /// <summary>
        /// Функция удаления по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> DeleteOkFunc(IResultCollection<ITestDomain> testDomains) =>
            id => testDomains.ResultValueOk(tests => SearchInDomains.FirstDomain(tests, id));

        /// <summary>
        /// Функция удаления по идентификатору.Элемент не найден
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> DeleteNotFoundFunc() =>
            _ => new ResultValue<ITestDomain>(ErrorData.NotFoundErrorType);

    }
}