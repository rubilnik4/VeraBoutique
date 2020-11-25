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
    /// Тестовые сервис проверки данных
    /// </summary>
    public class DatabaseServiceValidateMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, ValidateValueOkFunc(testDomains), ValidateCollectionOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains, 
                                                                      Func<ITestDomain, IResultError> validateValueFunc) =>
            GetTestDatabaseTable(testDomains, validateValueFunc, ValidateCollectionOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<ITestDomain, IResultError> validateValueFunc,
                                                                      Func<IResultError> validateCollectionFunc) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.GetShort()).
                                            ReturnsAsync(testDomains)).
            Void(serviceMock => serviceMock.Setup(service => service.Validate(It.IsAny<ITestDomain>())).
                                            ReturnsAsync(validateValueFunc)).
            Void(serviceMock => serviceMock.Setup(service => service.Validate(It.IsAny<IEnumerable<ITestDomain>>())).
                                            ReturnsAsync(validateCollectionFunc));

        /// <summary>
        /// Функция проверки значения
        /// </summary>
        public static Func<ITestDomain, IResultError> ValidateValueOkFunc(IResultCollection<ITestDomain> testDomains) =>
            domain => testDomains.ResultValueOk(tests => SearchInDomains.FirstDomain(tests, domain.Id).Id);

        /// <summary>
        /// Функция проверки значения. Элемент не найден
        /// </summary>
        public static Func<ITestDomain, IResultError> ValidateValueNotFoundFunc() =>
            _ => new ResultValue<TestEnum>(ErrorData.NotFoundError);

        /// <summary>
        /// Функция проверки коллекции
        /// </summary>
        public static Func<IResultError> ValidateCollectionOkFunc(IResultCollection<ITestDomain> testDomains) =>
            () => testDomains.ResultCollectionOk(TestData.GetTestIds);
    }
}