using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Identities
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public interface IUserManagerService
    {
        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        Task<IReadOnlyCollection<IBoutiqueUserDomain>> GetRoleUsers();

        /// <summary>
        /// Найти пользователя с ролью по почте
        /// </summary>
        Task<IResultValue<IBoutiqueUserDomain>> FindRoleUserByEmail(string email);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> CreateRoleUser(IRegisterRoleDomain registerRole);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(string email);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(IBoutiqueUserDomain user);
    }
}