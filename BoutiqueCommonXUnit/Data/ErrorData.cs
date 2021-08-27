using System.Collections.Generic;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

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
            new CommonErrorResult(CommonErrorType.Unknown, "Unknown error");

        /// <summary>
        /// Тестовый экземпляр ошибки
        /// </summary>
        public static IErrorResult ErrorTestFirst =>
            new CommonErrorResult(CommonErrorType.Unknown, "Unknown error first");

        /// <summary>
        /// Тестовый экземпляр ошибки
        /// </summary>
        public static IErrorResult ErrorTestSecond =>
            new CommonErrorResult(CommonErrorType.Unknown, "Unknown error second");

        /// <summary>
        /// Тестовые экземпляры ошибок
        /// </summary>
        public static IList<IErrorResult> ErrorsTest =>
            new List<IErrorResult>
            {
                ErrorTestFirst,
                ErrorTestSecond,
            };
    }
}