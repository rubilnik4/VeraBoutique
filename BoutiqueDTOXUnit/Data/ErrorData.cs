using System.Linq;
using System.Net;
using System.Net.Http;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using Functional.FunctionalExtensions.Sync;
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
            new HttpResponseMessage(HttpStatusCode.BadRequest).
            Map(response => response.ToRestResultError()).
            Errors.First();
    }
}