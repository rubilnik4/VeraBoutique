using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.CommonErrors
{
    public class CommonErrorResult : ErrorTypeResult<CommonErrorType>, IErrorTypeResult<CommonErrorType>
    {
        public CommonErrorResult(CommonErrorType commonErrorType, string description)
            : this(commonErrorType, description, null)
        { }

        public CommonErrorResult(CommonErrorType commonErrorType, string description, Exception? exception)
            : base(commonErrorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new CommonErrorResult(ErrorType, description, exception);
    }
}