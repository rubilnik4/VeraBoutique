using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace FunctionalXUnit.Data
{
    /// <summary>
    /// Тестовые экземпляры исключений
    /// </summary>
    public static class Exceptions
    {
        /// <summary>
        /// Вернуть ошибку на основании исключения
        /// </summary>
        public static IErrorResult ExceptionError() =>
            new ErrorTypeResult<CommonErrorType>(CommonErrorType.Unknown, "Деление на ноль", new DivideByZeroException());
    }
}