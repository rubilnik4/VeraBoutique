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
    public static class TestValidateServicePutMock
    {
        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService() =>
            GetDatabaseValidateService(ValidValueFunc());

        /// <summary>
        /// Получить сервис проверки данных
        /// </summary>
        public static Mock<ITestDatabaseValidateService> GetDatabaseValidateService(Func<ITestDomain, IResultError> validateValueFunc) =>
            new Mock<ITestDatabaseValidateService>().
            Void(validateMock => validateMock.Setup(validate => validate.ValidatePut(It.IsAny<ITestDomain>())).
                                              ReturnsAsync(validateValueFunc));

        /// <summary>
        /// Проверенные записи
        /// </summary>
        public static Func<ITestDomain, IResultError> ValidValueFunc() =>
            _ => new ResultError();

        /// <summary>
        /// Запись не найдена
        /// </summary>
        public static Func<ITestDomain, IResultError> ValueNotFoundFunc() =>
            testDomain => new ResultError(DatabaseErrorData.NotFoundError);
    }
}