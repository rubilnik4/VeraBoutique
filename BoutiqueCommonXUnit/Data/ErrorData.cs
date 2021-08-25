using System.Collections.Generic;
using Functional.Models.Enums;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Тестовые ошибки
    /// </summary>
    public static class ErrorData
    {
        /// <summary>
        /// Тестовый экземпляр ошибки
        /// </summary>
        public static IErrorResult ErrorTypeTest =>
            new ErrorResult(ErrorResultType.Unknown, "Unknown error");

        /// <summary>
        /// Создать тестовый экземпляр ошибки не найденного элемента
        /// </summary>
        public static IErrorResult NotFoundErrorType =>
            new ErrorResult(ErrorResultType.ValueNotFound, "NotFound");

        /// <summary>
        /// Создать тестовый экземпляр ошибки базы данных
        /// </summary>
        public static IErrorResult DatabaseErrorType =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, "DatabaseTableAccess");

        /// <summary>
        /// Тестовые экземпляры ошибок
        /// </summary>
        public static IList<IErrorResult> ErrorsTest =>
            new List<IErrorResult>
            {
                ErrorTypeTest,
                NotFoundErrorType,
            };
    }
}