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
            new HttpResponseMessage(HttpStatusCode.BadRequest).
            Map(response => response.ToRestResultError()).
            Errors.First();

        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeAuthorizeError =>
            new HttpResponseMessage(HttpStatusCode.Unauthorized).
            Map(response => response.ToRestResultError()).
            Errors.First();

        /// <summary>
        /// Тестовый экземпляр ошибки ответа сервера
        /// </summary>
        public static IErrorResult ErrorTypeInternalError =>
            new HttpResponseMessage(HttpStatusCode.InternalServerError).
            Map(response => response.ToRestResultError()).
            Errors.First();
    }
}