using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Infrastructure.Interfaces.Identities
{
    /// <summary>
    /// Менеджер ролей
    /// </summary>
    public interface IRoleStoreBoutique
    {
        /// <summary>
        /// Получить роли
        /// </summary>
        Task<IReadOnlyCollection<IdentityRole>> GetRoles();

        /// <summary>
        /// Создать роль
        /// </summary>
        Task<IdentityResult> CreateAsync(IdentityRole role);

        /// <summary>
        /// Поиск по имени
        /// </summary>
        Task<IdentityRole?> FindByNameAsync(string normalizedNamed);
    }
}