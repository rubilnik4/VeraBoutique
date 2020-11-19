using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueMVCXUnit.Data.Database.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueMVCXUnit.Controllers.Base.Mocks
{
    /// <summary>
    /// Тестовые сервис отправки данных
    /// </summary>
    public static class DatabaseServicePostMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, PostValueOkFunc(testDomains), PostCollectionOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<ITestDomain, IResultValue<TestEnum>> postValueFunc) =>
            GetTestDatabaseTable(testDomains, postValueFunc, PostCollectionOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<ITestDomain, IResultValue<TestEnum>> postValueFunc,
                                                                      Func<IResultCollection<TestEnum>> postCollectionFunc) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.Post(It.IsAny<ITestDomain>())).
                                            ReturnsAsync(postValueFunc)).
            Void(serviceMock => serviceMock.Setup(service => service.Post(It.IsAny<IEnumerable<ITestDomain>>())).
                                            ReturnsAsync(postCollectionFunc));

        /// <summary>
        /// Функция записи значения
        /// </summary>
        public static Func<ITestDomain, IResultValue<TestEnum>> PostValueOkFunc(IResultCollection<ITestDomain> testDomains) =>
            domain => testDomains.ResultValueOk(tests => SearchInModels.FirstDomain(tests, domain.Id).Id);

        /// <summary>
        /// Функция записи значения. Элемент не найден
        /// </summary>
        public static Func<ITestDomain, IResultValue<TestEnum>> PostValueFoundFunc() =>
            _ => new ResultValue<TestEnum>(ErrorData.NotFoundError);

        /// <summary>
        /// Функция записи коллекции
        /// </summary>
        public static Func<IResultCollection<TestEnum>> PostCollectionOkFunc(IResultCollection<ITestDomain> testDomains) =>
            () => testDomains.ResultCollectionOk(TestData.GetTestIds);
    }
}