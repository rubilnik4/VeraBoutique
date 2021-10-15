using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Interfaces.RestClients;

namespace BoutiqueConsole.Factories.Services
{
    public static class UserRestServiceFactory
    {
        /// <summary>
        /// Получить сервис категорий одежды
        /// </summary>
        public static IUserRestService GetUserRestService(IRestHttpClient restHttpClient) =>
             new UserRestService(restHttpClient, new RegisterTransferConverter(new AuthorizeTransferConverter(),
                                                                               new PersonalTransferConverter()),
                                 new BoutiqueUserTransferConverter());
    }
}