using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис регистрации
    /// </summary>
    public class RegisterRestService: RestService<string, IRegisterDomain, RegisterTransfer>, IRegisterRestService
    {
        public RegisterRestService(IRestHttpClient restHttpClient, IRegisterTransferConverter registerTransferConverter)
        {
            _restHttpClient = restHttpClient;
            _registerTransferConverter = registerTransferConverter;
        }

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        private readonly IRestHttpClient _restHttpClient;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly IRegisterTransferConverter _registerTransferConverter;

        /// <summary>
        /// Зарегистрироваться в сервисе
        /// </summary>
        public async Task<IResultValue<string>> Register(IRegisterDomain registerDomain) =>
            await _registerTransferConverter.ToTransfer(registerDomain).
            ToJsonTransfer().
            ResultValueBindOkAsync(json => _restHttpClient.PostAsync(RestRequest.PostRequest(ControllerName), json));
    }
}