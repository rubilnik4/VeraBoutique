using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueCommon.Models.Enums.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity
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
        /// Получить роли для пользователя
        /// </summary>
        Task<BoutiqueRoleUser> GetRoleUser(BoutiqueIdentityUser user);

        /// <summary>
        /// Получить роли пользователей
        /// </summary>
        Task<IList<string>> GetRoles(BoutiqueIdentityUser user);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IResultValue<string>> DeleteRoleUser(BoutiqueIdentityUser user);

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        Task<IResultValue<BoutiqueIdentityUser>> FindByEmail(string email);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> Register(IRegisterDomain register, IdentityRoleType roleType);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IResultValue<string>> Register(BoutiqueIdentityUser user, IdentityRoleType roleType);
    }
}