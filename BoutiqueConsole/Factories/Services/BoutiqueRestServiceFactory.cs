using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueConsole.Factories.Services
{
    /// <summary>
    /// Фабрика создания сервисов для получения и загрузки данных Api
    /// </summary>
    public static class BoutiqueRestServiceFactory
    {
        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static IRestHttpClient GetBoutiqueRestClient(IHostConfigurationDomain hostConfiguration) =>
            new RestHttpClient(hostConfiguration.Host, hostConfiguration.TimeOut);

        /// <summary>
        /// Клиент для подключения к сервису одежды
        /// </summary>
        public static IRestJwtHttpClient GetBoutiqueRestClient(IHostConfigurationDomain hostConfiguration, string jwtToken) =>
            new RestJwtHttpClient(hostConfiguration.Host, hostConfiguration.TimeOut, () => Task.FromResult((string?)jwtToken));

        /// <summary>
        ///  Получить сервис авторизации
        /// </summary>
        public static IAuthorizeRestService GetAuthorizeRestService(IRestHttpClient restHttpClient) =>
             new AuthorizeRestService(restHttpClient, new AuthorizeTransferConverter());
    }
}