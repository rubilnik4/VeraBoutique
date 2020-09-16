using System.Collections.Generic;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

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
        public static IErrorResult ErrorTest =>
            new ErrorResult(ErrorResultType.Unknown, "Unknown error");

        /// <summary>
        /// Создать тестовый экземпляр ошибки не найденного элемента
        /// </summary>
        public static IErrorResult NotFoundError =>
            new ErrorResult(ErrorResultType.DatabaseValueNotFound, "NotFound");

        /// <summary>
        /// Создать тестовый экземпляр ошибки базы данных
        /// </summary>
        public static IErrorResult DatabaseError =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, "DatabaseTableAccess");

        /// <summary>
        /// Тестовые экземпляры ошибок
        /// </summary>
        public static IList<IErrorResult> ErrorsTest =>
            new List<IErrorResult>
            {
                ErrorTest,
                NotFoundError,
            };
    }
}