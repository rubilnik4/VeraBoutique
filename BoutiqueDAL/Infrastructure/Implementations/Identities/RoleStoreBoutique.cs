using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Identities
{
    /// <summary>
    /// Менеджер ролей
    /// </summary>
    public class RoleStoreBoutique : RoleStore<IdentityRole>, IRoleStoreBoutique
    {
        public RoleStoreBoutique(BoutiqueDatabase dbContext)
            : base(dbContext)
        { }

        /// <summary>
        /// Получить роли
        /// </summary>
        public async Task<IReadOnlyCollection<IdentityRole>> GetRoles() =>
            await Roles.ToListAsync();

        /// <summary>
        /// Создать роль
        /// </summary> 
        public async Task<IdentityResult> CreateAsync(IdentityRole role) =>
            await CreateAsync(role, default);

        /// <summary>
        /// Поиск по имени
        /// </summary>
        public async Task<IdentityRole?> FindByNameAsync(string normalizedNamed) =>
            await FindByNameAsync(normalizedNamed, default);
    }
}