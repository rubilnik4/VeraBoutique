using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTO.Models.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис регистрации
    /// </summary>
    public class UserRestService: RestService<string, IRegisterDomain, RegisterTransfer>, IUserRestService
    {
        public UserRestService(IRestHttpClient restHttpClient, IRegisterTransferConverter registerTransferConverter, 
                               IBoutiqueUserTransferConverter boutiqueUserTransferConverter)
        {
            _restHttpClient = restHttpClient;
            _registerTransferConverter = registerTransferConverter;
            _boutiqueUserTransferConverter = boutiqueUserTransferConverter;
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
        /// Конвертер пользователей в трансферную модель
        /// </summary>
        private readonly IBoutiqueUserTransferConverter _boutiqueUserTransferConverter;

        /// <summary>
        /// Получить пользователей
        /// </summary>
        public async Task<IResultCollection<IBoutiqueUserDomain>> GetUsers() =>
            await _restHttpClient.GetCollectionAsync<BoutiqueUserTransfer>(RestRequest.GetRequest(ControllerName)).
            ResultCollectionBindOkTaskAsync(users => _boutiqueUserTransferConverter.FromTransfers(users));

        /// <summary>
        /// Зарегистрироваться в сервисе
        /// </summary>
        public async Task<IResultValue<string>> Register(IRegisterDomain registerDomain) =>
            await _registerTransferConverter.ToTransfer(registerDomain).
            ToJsonTransfer().
            ResultValueBindOkAsync(json => _restHttpClient.PostAsync(RestRequest.PostRequest(ControllerName), json));

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        public async Task<IResultError> DeleteUsers() =>
             await _restHttpClient.DeleteCollectionAsync(RestRequest.GetRequest(ControllerName));

    }
}