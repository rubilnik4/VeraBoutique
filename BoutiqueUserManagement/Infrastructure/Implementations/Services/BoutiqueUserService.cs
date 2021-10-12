using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueConsole.Factories.Services;
using BoutiqueConsole.Infrastructure.Services.Authorize;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueUserManagement.Infrastructure.Implementations.Services
{
    /// <summary>
    /// Сервис удаления пользователей
    /// </summary>
    public static class BoutiqueUserService
    {
        /// <summary>
        /// Авторизироваться и удалить пользователей
        /// </summary>
        public static async Task<IResultError> DeleteUsers(IBoutiqueLogger boutiqueLogger) =>
            await BoutiqueAuthorizeService.AuthorizeJwt(boutiqueLogger).
            ResultValueBindOkBindAsync(token => BoutiqueRestServiceFactory.GetBoutiqueRestClient(token, boutiqueLogger)).
    }
}