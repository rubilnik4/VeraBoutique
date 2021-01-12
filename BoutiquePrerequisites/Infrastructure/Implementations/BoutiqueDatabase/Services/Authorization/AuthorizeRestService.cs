using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Authorization;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Authorization
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AuthorizeRestService : IAuthorizeRestService
    {
        public AuthorizeRestService(IAuthorizeApiService authorizeApiService,
                                    IAuthorizeTransferConverter authorizeTransferConverter,
                                    ILogger logger)
        {
            _authorizeApiService = authorizeApiService;
            _authorizeTransferConverter = authorizeTransferConverter;
            _logger = logger;
        }

        /// <summary>
        /// Сервис получения данных по протоколу rest api
        /// </summary>
        private readonly IAuthorizeApiService _authorizeApiService;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly IAuthorizeTransferConverter _authorizeTransferConverter;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Авторизироваться в сервисе
        /// </summary>
        public async Task<IResultValue<string>> AuthorizeJwt(IAuthorizeDomain authorizeDomain) =>
            await new ResultValue<IAuthorizeApiService>(_authorizeApiService).
            ResultValueVoidOk(_ => _logger.ShowMessage($"Авторизация в сервисе [{_authorizeApiService.ControllerName}]")).
            ResultValueBindOkAsync(api => api.AuthorizeJwt(_authorizeTransferConverter.ToTransfer(authorizeDomain))).
            ResultValueVoidOkBadTaskAsync(ids => ids.Void(_ => _logger.ShowMessage($"Токен сервиса [{_authorizeApiService.ControllerName}] получен")),
                                               errors => errors.
                                                         Void(_ => _logger.ShowMessage($"Ошибка авторизации в сервисе [{_authorizeApiService.ControllerName}]")).
                                                         Void(_ => _logger.ShowErrors(errors)));
    }
}