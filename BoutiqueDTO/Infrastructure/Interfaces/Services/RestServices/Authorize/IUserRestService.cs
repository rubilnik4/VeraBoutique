using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис регистрации
    /// </summary>
    public interface IUserRestService
    {
        /// <summary>
        /// Получить пользователей
        /// </summary>
        Task<IResultCollection<IBoutiqueUserDomain>> GetUsers();

        /// <summary>
        /// Зарегистрироваться в сервисе
        /// </summary>
        Task<IResultValue<string>> Register(IRegisterDomain registerDomain);

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        Task<IResultError> DeleteUsers();
    }
}