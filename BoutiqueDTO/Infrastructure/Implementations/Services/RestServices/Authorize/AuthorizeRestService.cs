using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AuthorizeRestService : RestService<string, IAuthorizeDomain, AuthorizeTransfer>,
                                        IAuthorizeRestService
    {
        public AuthorizeRestService(IRestHttpClient restHttpClient, IAuthorizeTransferConverter authorizeTransferConverter)
        {
            _restHttpClient = restHttpClient;
            _authorizeTransferConverter = authorizeTransferConverter;
        }

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        private readonly IRestHttpClient _restHttpClient;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly IAuthorizeTransferConverter _authorizeTransferConverter;

        /// <summary>
        /// Авторизироваться в сервисе
        /// </summary>
        public async Task<IResultValue<string>> AuthorizeJwt(IAuthorizeDomain authorizeDomain) =>
            await _authorizeTransferConverter.ToTransfer(authorizeDomain).
            ToJsonTransfer().
            ResultValueBindOkAsync(json => _restHttpClient.PostAsync(RestRequest.PostRequest(ControllerName), json)).
            ResultValueBindBadTaskAsync(errors => ToTokenError(errors).ToResultValue<string>());

        /// <summary>
        /// Преобразовать в ошибку токена
        /// </summary>
        private static IErrorResult ToTokenError(IEnumerable<IErrorResult> errors) =>
            errors.FirstOrDefault() switch
            {
                RestMessageErrorResult { ErrorType: RestErrorType.Unauthorized } =>
                    ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Token, "Введены неверные имя пользователя и пароль"),
                { } error => error,
            };
    }
}