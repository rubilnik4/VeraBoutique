using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate
{
    /// <summary>
    /// Базовый сервис проверки данных из базы
    /// </summary>
    public static class TestValidateServicePostMock
    {
        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService() =>
            GetDatabaseValidateService(ValidValueFunc(), ValidCollectionFunc());

        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService(Func<ITestDomain, IResultError> validateValueFunc) =>
            GetDatabaseValidateService(validateValueFunc, ValidCollectionFunc());

        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService(Func<IEnumerable<ITestDomain>, IResultError> validateCollectionFunc) =>
            GetDatabaseValidateService(ValidValueFunc() , validateCollectionFunc);

        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService(Func<ITestDomain, IResultError> validateValueFunc,
                                                                                    Func<IEnumerable<ITestDomain>, IResultError> validateCollectionFunc) =>
            new Mock<ITestDatabaseValidateService>().
            Void(validateMock => validateMock.Setup(validate => validate.ValidatePost(It.IsAny<ITestDomain>())).
                                              ReturnsAsync(validateValueFunc)).
            Void(validateMock => validateMock.Setup(validate => validate.ValidatePost(It.IsAny<IEnumerable<ITestDomain>>())).
                                              ReturnsAsync(validateCollectionFunc));

        /// <summary>
        /// Проверенные записи
        /// </summary>
        public static Func<ITestDomain, IResultError> ValidValueFunc() =>
            _ => new ResultError();

        /// <summary>
        /// Проверенные записи
        /// </summary>
        public static Func<IEnumerable<ITestDomain>, IResultError> ValidCollectionFunc() =>
            _ => new ResultError();

        /// <summary>
        /// Дублирование записей
        /// </summary>
        public static Func<ITestDomain, IResultError> DuplicateValueFunc() =>
            testDomain => new ResultError(DatabaseErrorData.GetDuplicateError(testDomain.Id));

        /// <summary>
        /// Дублирование записей
        /// </summary>
        public static Func<IEnumerable<ITestDomain>, IResultError> DuplicateCollectionFunc() =>
            testDomains => new ResultError(DatabaseErrorData.GetDuplicateError(testDomains));
    }
}