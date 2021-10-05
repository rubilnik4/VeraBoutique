using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identity;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Identity
{
    /// <summary>
    /// Управление ролями
    /// </summary>
    public interface IRoleStoreBoutique: IRoleStore<IdentityRole>
    {
        /// <summary>
        /// Создать роль
        /// </summary>
        Task<IResultValue<IdentityRoleType>> CreateRole(IdentityRoleType identityRoleType);
    }
}