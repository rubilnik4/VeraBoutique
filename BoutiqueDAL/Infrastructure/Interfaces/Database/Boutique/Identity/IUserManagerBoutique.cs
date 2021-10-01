using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public interface IUserManagerBoutique
    {
        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IdentityResult> Register(IRegisterDomain register, IdentityRoleType roleType);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IdentityResult> Register(BoutiqueUser user, IdentityRoleType roleType);

        /// <summary>
        /// Получить роли пользователей
        /// </summary>
        Task<IList<string>> GetRolesAsync(BoutiqueUser user);

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        Task<BoutiqueUser?> FindByEmail(string email);
    }
}