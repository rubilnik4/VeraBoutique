using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
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
        IQueryable<BoutiqueUser> Users { get; }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<IdentityResult> Register(IRegisterDomain register);

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