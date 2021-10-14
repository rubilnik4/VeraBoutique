using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueMVCXUnit.Data.Database.Interfaces;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
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
                                                                      Func<TestEnum, IResultValue<TestEnum>> deleteFunc) =>
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
                                                                      Func<TestEnum, IResultValue<TestEnum>> deleteFunc,
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
        public static Func<TestEnum, IResultValue<TestEnum>> DeleteOkFunc(IResultCollection<ITestDomain> testDomains) =>
            id => testDomains.ResultValueOk(tests => SearchInDomains.FirstDomain(tests, id).Id);

        /// <summary>
        /// Функция удаления по идентификатору.Элемент не найден
        /// </summary>
        public static Func<TestEnum, IResultValue<TestEnum>> DeleteNotFoundFunc() =>
            _ => ErrorData.ErrorNotFound.ToResultValue<TestEnum>();

    }
}