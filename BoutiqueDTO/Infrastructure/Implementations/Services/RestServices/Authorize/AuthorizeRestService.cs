﻿using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.RestClients;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AuthorizeRestService : RestService<(string, string), IAuthorizeDomain, AuthorizeTransfer>,
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
            MapTaskAsync(tt => tt);
    }
}