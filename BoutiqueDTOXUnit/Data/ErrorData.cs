using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace BoutiqueDTOXUnit.Data
{
    public class ErrorTransferData
    {
        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeBadRequest =>
            ErrorResultFactory.RestError(RestErrorType.BadRequest, "localhost:5000", "BadRequest");
    }
}