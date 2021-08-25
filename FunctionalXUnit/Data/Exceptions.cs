using System;
using Functional.Models.Enums;

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
            new ErrorResult(ErrorResultType.DivideByZero, "Деление на ноль");
    }
}