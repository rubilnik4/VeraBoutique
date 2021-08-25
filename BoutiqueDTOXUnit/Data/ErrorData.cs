using Functional.Models.Enums;

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