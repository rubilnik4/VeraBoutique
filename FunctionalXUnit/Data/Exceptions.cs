using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

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
        public static IErrorResult FuncExceptionToError(Exception exception) =>
            new ErrorResult(ErrorResultType.DevideByZero, "Деление на ноль", exception);
    }
}