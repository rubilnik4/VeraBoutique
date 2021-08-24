using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate
{
    public static class TestIncludeValidateServiceMock
    {
        /// <summary>
        /// Тестовый сервис проверки вложенных данных
        /// </summary>
        public static Mock<ITestIncludeDatabaseValidateService> GetTestIncludeValidateService() =>
            GetTestIncludeValidateService(ValidateFindsOk());

        /// <summary>
        /// Тестовый сервис проверки вложенных данных
        /// </summary>
        public static Mock<ITestIncludeDatabaseValidateService> GetTestIncludeValidateService(Func<IEnumerable<string>, IResultError> validateFindsFunc) =>
             new Mock<ITestIncludeDatabaseValidateService>().
                 Void(validateMock => validateMock.Setup(validate => validate.ValidateFinds(It.IsAny<IEnumerable<string>>())).
                                                   ReturnsAsync(validateFindsFunc));

        /// <summary>
        /// Функция проверки наличия
        /// </summary>
        public static Func<IEnumerable<string>, IResultError> ValidateFindsOk() =>
            _ => new ResultError();

        /// <summary>
        /// Функция проверки наличия
        /// </summary>
        public static Func<IEnumerable<string>, IResultError> ValidateFindsError() =>
            _ => new ResultError(ErrorData.NotFoundErrorType);
    }
}