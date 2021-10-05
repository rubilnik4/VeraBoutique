using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Identity
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public interface IUserManagerBoutique
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
        /// Получить роли для пользователя
        /// </summary>
        Task<BoutiqueRoleUser> GetRoleUser(BoutiqueIdentityUser user);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(BoutiqueIdentityUser user);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> CreateRoleUser(IRegisterDomain register, IdentityRoleType roleType);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> CreateRoleUser(BoutiqueIdentityUser user, IdentityRoleType roleType);
    }
}