using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Models.Interfaces.Identity
{
    /// <summary>
    /// Менеджер авторизации
    /// </summary>
    public interface IUserManagerBoutique
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        IQueryable<IdentityUser> Users { get; }

        /// <summary>
        /// Получить роли пользователей
        /// </summary>
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}