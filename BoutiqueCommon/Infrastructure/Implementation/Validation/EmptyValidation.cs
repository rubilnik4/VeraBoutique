using System;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueCommon.Infrastructure.Implementation.Validation
{
    /// <summary>
    /// Проверка на пустые строки
    /// </summary>
    public static class EmptyValidation
    {
        /// <summary>
        /// Проверить строку на корректность написания
        /// </summary>
        public static bool IsValid(string line) =>
            line.
            ToResultValueWhere(text => !String.IsNullOrWhiteSpace(text), GetValidationError).
            Map(result => result.OkStatus);

        /// <summary>
        /// Получить ошибку проверки
        /// </summary>
        private static IErrorResult GetValidationError(string line) =>
            ErrorResultFactory.ValueNotValidError(line, typeof(EmptyValidation), String.Empty);
    }
}