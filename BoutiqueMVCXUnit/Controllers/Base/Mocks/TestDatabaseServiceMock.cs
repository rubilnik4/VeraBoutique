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
    public static class TestDatabaseServiceMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных в стандартном исполнении
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, GetByIdOkFunc(testDomains), PostOkFunc(testDomains), 
                                 PutOkFunc(testDomains), DeleteOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных в стандартном исполнении
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTableGet(IResultCollection<ITestDomain> testDomains,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> getByIdFunc) =>
            GetTestDatabaseTable(testDomains, getByIdFunc, PostOkFunc(testDomains), 
                                 PutOkFunc(testDomains), DeleteOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных в стандартном исполнении
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTablePost(IResultCollection<ITestDomain> testDomains,
                                                                      Func<IResultCollection<TestEnum>> postFunc) =>
            GetTestDatabaseTable(testDomains, GetByIdOkFunc(testDomains), postFunc, 
                                 PutOkFunc(testDomains), DeleteOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных в стандартном исполнении
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTablePut(IResultCollection<ITestDomain> testDomains,
                                                                      Func<IResultError> putFunc) =>
            GetTestDatabaseTable(testDomains, GetByIdOkFunc(testDomains), PostOkFunc(testDomains), putFunc, 
                                 DeleteOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных в стандартном исполнении
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTableDelete(IResultCollection<ITestDomain> testDomains,
                                                                            Func<TestEnum, IResultValue<ITestDomain>> deleteFunc) =>
            GetTestDatabaseTable(testDomains, GetByIdOkFunc(testDomains), PostOkFunc(testDomains),
                                 PutOkFunc(testDomains), deleteFunc);

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> getByIdFunc,
                                                                      Func<IResultCollection<TestEnum>> postFunc,
                                                                      Func<IResultError> putFunc,
                                                                      Func<TestEnum, IResultValue<ITestDomain>> deleteFunc) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.Get()).
                                            ReturnsAsync(testDomains)).
            Void(serviceMock => serviceMock.Setup(service => service.Get(It.IsAny<TestEnum>())).
                                            ReturnsAsync(getByIdFunc)).
            Void(serviceMock => serviceMock.Setup(service => service.Post(It.IsAny<IReadOnlyCollection<ITestDomain>>())).
                                            ReturnsAsync(postFunc)).
            Void(serviceMock => serviceMock.Setup(service => service.Put(It.IsAny<ITestDomain>())).
                                            ReturnsAsync(putFunc)).
            Void(serviceMock => serviceMock.Setup(service => service.Delete(It.IsAny<TestEnum>())).
                                            ReturnsAsync(deleteFunc));

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> GetByIdOkFunc(IResultCollection<ITestDomain> testDomains) =>
            id => testDomains.ResultValueOk(tests => SearchInModels.FirstDomain(tests, id));

        /// <summary>
        /// Функция поиска по идентификатору.Элемент не найден
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> GetByIdNotFoundFunc() =>
            id =>  new ResultValue<ITestDomain>(ErrorData.NotFoundError);

        /// <summary>
        /// Функция записи
        /// </summary>
        public static Func<IResultCollection<TestEnum>> PostOkFunc(IResultCollection<ITestDomain> testDomains) =>
            () => testDomains.ResultCollectionOk(TestData.GetTestIds);

        /// <summary>
        /// Функция изменения
        /// </summary>
        public static Func<IResultError> PutOkFunc(IResultError testResult) =>
            () => testResult;

        /// <summary>
        /// Функция изменения. Элемент не найден
        /// </summary>
        public static Func<IResultError> PutNotFoundFunc() =>
            () => new ResultError(ErrorData.NotFoundError);

        /// <summary>
        /// Функция удаления по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> DeleteOkFunc(IResultCollection<ITestDomain> testDomains) =>
            id => testDomains.ResultValueOk(tests => SearchInModels.FirstDomain(tests, id));

        /// <summary>
        /// Функция удаления по идентификатору.Элемент не найден
        /// </summary>
        public static Func<TestEnum, IResultValue<ITestDomain>> DeleteNotFoundFunc() =>
            id => new ResultValue<ITestDomain>(ErrorData.NotFoundError);
    }
}