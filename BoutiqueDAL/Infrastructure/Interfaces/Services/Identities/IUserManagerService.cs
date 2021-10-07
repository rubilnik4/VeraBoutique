using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
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
        /// Получить пользователей
        /// </summary>
        Task<IReadOnlyCollection<BoutiqueIdentityUser>> GetUsers();

        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        Task<IReadOnlyCollection<BoutiqueRoleUser>> GetRoleUsers();

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        Task<IResultValue<BoutiqueIdentityUser>> FindUserByEmail(string email);

        /// <summary>
        /// Найти пользователя с ролью по почте
        /// </summary>
        Task<IResultValue<BoutiqueRoleUser>> FindRoleUserByEmail(string email);

        /// <summary>
        /// Получить пользователя с ролью
        /// </summary>
        Task<BoutiqueRoleUser> GetRoleUser(BoutiqueIdentityUser user);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> CreateRoleUser(IRegisterDomain register, IdentityRoleType roleType);


        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> CreateRoleUser(BoutiqueIdentityUser user, IdentityRoleType roleType);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(string email);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(BoutiqueIdentityUser user);
    }
}