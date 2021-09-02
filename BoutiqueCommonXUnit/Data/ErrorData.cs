using System;
using System.Collections.Generic;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

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

        /// <summary>
        /// Тестовый экземпляр ошибки со статусом не найдено
        /// </summary>
        public static IErrorResult ErrorNotFound =>
           ErrorResultFactory.ValueNotFoundError(String.Empty, typeof(ErrorData));
    }
}