using Functional.Models.Enums;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTOXUnit.Data
{
    public class ErrorTransferData
    {
        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeBadRequest =>
            new ErrorResult(ErrorResultType.BadRequest, "BadRequest");
    }
}