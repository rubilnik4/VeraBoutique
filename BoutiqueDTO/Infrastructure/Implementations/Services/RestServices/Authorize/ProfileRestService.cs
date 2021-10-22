using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize
{
    public class ProfileRestService : RestService<string, IBoutiqueUserDomain, BoutiqueUserTransfer>, IProfileRestService
    {
        public ProfileRestService(IRestHttpClient restHttpClient, IBoutiqueUserTransferConverter boutiqueUserTransferConverter)
        {
            _restHttpClient = restHttpClient;
            _boutiqueUserTransferConverter = boutiqueUserTransferConverter;
        }

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        private readonly IRestHttpClient _restHttpClient;

        /// <summary>
        /// Конвертер пользователей в трансферную модель
        /// </summary>
        private readonly IBoutiqueUserTransferConverter _boutiqueUserTransferConverter;

        /// <summary>
        /// Получить личные данные пользователя
        /// </summary>
        public async Task<IResultValue<IBoutiqueUserDomain>> GetProfile() =>
            await _restHttpClient.GetValueAsync<BoutiqueUserTransfer>(RestRequest.GetRequest(ControllerName)).
            ResultValueBindOkTaskAsync(users => _boutiqueUserTransferConverter.FromTransfer(users));
    }
}