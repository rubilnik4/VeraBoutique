using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueMVCXUnit.Data.Database.Interfaces;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
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
            GetTestDatabaseTable(PostValueOkFunc(testDomains), PostCollectionOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<ITestDomain, IResultValue<TestEnum>> postValueFunc) =>
            GetTestDatabaseTable(postValueFunc, PostCollectionOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(Func<ITestDomain, IResultValue<TestEnum>> postValueFunc,
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
            domain => testDomains.ResultValueOk(tests => SearchInDomains.FirstDomain(tests, domain.Id).Id);

        /// <summary>
        /// Функция записи значения. Элемент не найден
        /// </summary>
        public static Func<ITestDomain, IResultValue<TestEnum>> PostValueFoundFunc() =>
            _ => ErrorData.ErrorNotFound.ToResultValue<TestEnum>();

        /// <summary>
        /// Функция записи коллекции
        /// </summary>
        public static Func<IResultCollection<TestEnum>> PostCollectionOkFunc(IResultCollection<ITestDomain> testDomains) =>
            () => testDomains.ResultCollectionOk(TestData.GetTestIds);
    }
}