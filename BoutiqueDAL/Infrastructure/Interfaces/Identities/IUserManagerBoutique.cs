using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Infrastructure.Interfaces.Identities
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public interface IUserManagerBoutique
    {
        /// <summary>
        /// Получить пользователей
        /// </summary>
        Task<IReadOnlyCollection<BoutiqueUserEntity>> GetUsers();

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        Task<BoutiqueUserEntity> FindByEmailAsync(string email);

        /// <summary>
        /// получить пользователей по роли
        /// </summary>
        Task<IList<BoutiqueUserEntity>> GetUsersInRoleAsync(string roleName);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IdentityResult> CreateAsync(BoutiqueUserEntity userEntity);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IdentityResult> DeleteAsync(BoutiqueUserEntity userEntity);

        /// <summary>
        /// Получить роли пользователя
        /// </summary>
        Task<IList<string>> GetRolesAsync(BoutiqueUserEntity userEntity);

        /// <summary>
        /// Создать роль у пользователя
        /// </summary>
        Task<IdentityResult> AddToRoleAsync(BoutiqueUserEntity userEntity, string role);

        /// <summary>
        /// Удалить роль у пользователя
        /// </summary>
        Task<IdentityResult> RemoveFromRoleAsync(BoutiqueUserEntity userEntity, string role);
    }
}