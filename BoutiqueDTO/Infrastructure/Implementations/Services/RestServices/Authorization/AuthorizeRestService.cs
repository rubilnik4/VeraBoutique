using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorization;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AuthorizeRestService : IAuthorizeRestService
    {
        public AuthorizeRestService(IAuthorizeApiService authorizeApiService,
                                    IAuthorizeTransferConverter authorizeTransferConverter)
        {
            _authorizeApiService = authorizeApiService;
            _authorizeTransferConverter = authorizeTransferConverter;
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
        /// Авторизироваться в сервисе
        /// </summary>
        public async Task<IResultValue<string>> AuthorizeJwt(IAuthorizeDomain authorizeDomain) =>
            await new ResultValue<IAuthorizeApiService>(_authorizeApiService).
            ResultValueBindOkAsync(_ => _authorizeApiService.AuthorizeJwt(_authorizeTransferConverter.ToTransfer(authorizeDomain)));
    }
}