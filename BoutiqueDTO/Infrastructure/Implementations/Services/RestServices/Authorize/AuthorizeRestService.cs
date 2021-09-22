﻿using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AuthorizeRestService : RestService<string, IAuthorizeDomain, AuthorizeTransfer>,
                                        IAuthorizeRestService
    {
        public AuthorizeRestService(IRestHttpClient restHttpClient,
                                    IAuthorizeTransferConverter authorizeTransferConverter)
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
            ResultValueBindBadTaskAsync(_ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Token, "Введены неверные имя пользователя и пароль").
                                                                ToResultValue<string>());
    }
}