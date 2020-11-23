using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks
{
    /// <summary>
    /// Базовый сервис проверки данных из базы
    /// </summary>
    public static class DatabaseValidateServiceMock
    {
        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService() =>
            GetDatabaseValidateService(NonDuplicateFunc(), ValidValueFunc(), NonDuplicatesFunc(), ValidCollectionFunc());

        /// <summary>
        /// Получить сервис проверки данных c дублированием
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateServiceDuplicate(Func<ITestDomain, IResultError> duplicateFunc) =>
            GetDatabaseValidateService(duplicateFunc, ValidValueFunc(), NonDuplicatesFunc(), ValidCollectionFunc());

        /// <summary>
        /// Получить сервис проверки данных c дублированием
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateServiceValidateValue(Func<ITestDomain, IResultError> validateValueFunc) =>
            GetDatabaseValidateService(NonDuplicateFunc(), validateValueFunc, NonDuplicatesFunc(), ValidCollectionFunc());

        /// <summary>
        /// Получить сервис проверки данных c дублированием
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateServiceDuplicates(Func<IReadOnlyCollection<ITestDomain>, IResultError> duplicatesFunc) =>
            GetDatabaseValidateService(NonDuplicateFunc(), ValidValueFunc(), duplicatesFunc, ValidCollectionFunc());

        /// <summary>
        /// Получить сервис проверки данных c дублированием
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateServiceValidateCollection(Func<IReadOnlyCollection<ITestDomain>, IResultError> validateCollectionFunc) =>
            GetDatabaseValidateService(NonDuplicateFunc(), ValidValueFunc(), NonDuplicatesFunc(), validateCollectionFunc);

        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService(Func<ITestDomain, IResultError> duplicateFunc,
                                                                                    Func<ITestDomain, IResultError> validateValueFunc,
                                                                                    Func<IReadOnlyCollection<ITestDomain>, IResultError> duplicatesFunc,
                                                                                    Func<IReadOnlyCollection<ITestDomain>, IResultError> validateCollectionFunc) =>
            new Mock<ITestDatabaseValidateService>().
            Void(validateMock => validateMock.Setup(validate => validate.ValidateDuplicate(It.IsAny<ITestDomain>())).
                                              ReturnsAsync(duplicateFunc)).
            Void(validateMock => validateMock.Setup(validate => validate.ValidateValue(It.IsAny<ITestDomain>())).
                                              ReturnsAsync(validateValueFunc)).
            Void(validateMock => validateMock.Setup(validate => validate.ValidateDuplicates(It.IsAny<IReadOnlyCollection<ITestDomain>>())).
                                              ReturnsAsync(duplicatesFunc)).
            Void(validateMock => validateMock.Setup(validate => validate.ValidateCollection(It.IsAny<IReadOnlyCollection<ITestDomain>>())).
                                              ReturnsAsync(validateCollectionFunc));

        /// <summary>
        /// Отсутствие дублирующих записей
        /// </summary>
        public static Func<ITestDomain, IResultError> NonDuplicateFunc() =>
            _ => new ResultError();

        /// <summary>
        /// Дублирование записей
        /// </summary>
        public static Func<ITestDomain, IResultError> DuplicateFunc() =>
            testDomain => new ResultError(Errors.GetDuplicateError(testDomain.Id));

        /// <summary>
        /// Отсутствие дублирующих записей
        /// </summary>
        public static Func<IReadOnlyCollection<ITestDomain>, IResultError> NonDuplicatesFunc() =>
            _ => new ResultError();

        /// <summary>
        /// Проверенные записи
        /// </summary>
        public static Func<ITestDomain, IResultError> ValidValueFunc() =>
            testDomain => new ResultError();

        /// <summary>
        /// Непроверенные записи
        /// </summary>
        public static Func<ITestDomain, IResultError> NonValidFunc() =>
            testDomain => new ResultError(Errors.NotFoundError);


        /// <summary>
        /// Дублирование записей
        /// </summary>
        public static Func<IReadOnlyCollection<ITestDomain>, IResultError> DuplicatesFunc() =>
            testDomains => new ResultError(Errors.GetDuplicateError(testDomains.Select(test => test.Id)));

        /// <summary>
        /// Проверенные записи
        /// </summary>
        public static Func<IReadOnlyCollection<ITestDomain>, IResultError> ValidCollectionFunc() =>
            _ => new ResultError();

        /// <summary>
        /// Непроверенные записи
        /// </summary>
        public static Func<IReadOnlyCollection<ITestDomain>, IResultError> NonValidCollectionFunc() =>
            _ => new ResultError(Errors.NotFoundError);
    }
}