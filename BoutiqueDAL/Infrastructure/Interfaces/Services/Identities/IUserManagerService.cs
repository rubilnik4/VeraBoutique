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
    /// Менеджер пользователей
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
        /// Получить пользователей по роли
        /// </summary>
        Task<IReadOnlyCollection<IBoutiqueUserDomain>> GetUsersByRole(IdentityRoleType identityRoleType);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task <IResultValue<string>> CreateRoleUser(IRegisterRoleDomain registerRole);
        
        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultError> UpdateRoleUser(IBoutiqueUserDomain user);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(string email);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(IBoutiqueUserDomain user);

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        Task<IResultError> DeleteRoleUsers(IEnumerable<IBoutiqueUserDomain> users);

        /// <summary>
        /// Удалить пользователей
        /// </summary>
        Task<IResultError> DeleteRoleUsersByRole(IdentityRoleType identityRoleType);
    }
}