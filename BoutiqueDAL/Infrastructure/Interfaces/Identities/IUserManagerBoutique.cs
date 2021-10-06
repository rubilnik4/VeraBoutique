using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
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
        /// Пользователи
        /// </summary>
        IQueryable<BoutiqueIdentityUser> Users { get; }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        Task<BoutiqueIdentityUser> FindByEmailAsync(string email);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IdentityResult> CreateAsync(BoutiqueIdentityUser user);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<IdentityResult> DeleteAsync(BoutiqueIdentityUser user);

        /// <summary>
        /// Получить роли пользователя
        /// </summary>
        Task<IList<string>> GetRolesAsync(BoutiqueIdentityUser user);

        /// <summary>
        /// Создать роль у пользователя
        /// </summary>
        Task<IdentityResult> AddToRoleAsync(BoutiqueIdentityUser user, string role);

        /// <summary>
        /// Удалить роль у пользователя
        /// </summary>
        Task<IdentityResult> RemoveFromRoleAsync(BoutiqueIdentityUser user, string role);
    }
}