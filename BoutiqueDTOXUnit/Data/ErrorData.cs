using System.Linq;
using System.Net;
using System.Net.Http;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueDTOXUnit.Data
{
    public class ErrorTransferData
    {
        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeBadRequest =>
            ErrorResultFactory.RestError(RestErrorType.BadRequest, new HttpResponseMessage(HttpStatusCode.BadRequest),
                                         RestErrorType.BadRequest.ToString());

        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeAuthorizeError =>
            ErrorResultFactory.RestError(RestErrorType.Unauthorized, new HttpResponseMessage(HttpStatusCode.Unauthorized),
                                         RestErrorType.Unauthorized.ToString());

        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeInternalError =>
             ErrorResultFactory.RestError(RestErrorType.InternalServerError, new HttpResponseMessage(HttpStatusCode.InternalServerError),
                                         RestErrorType.InternalServerError.ToString());
    }
}