using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using Microsoft.AspNetCore.Identity;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Identities
{
    /// <summary>
    /// Управление ролями
    /// </summary>
    public interface IRoleStoreService
    {
        /// <summary>
        /// Создать роль
        /// </summary>
        Task<IResultValue<IdentityRoleType>> CreateRole(IdentityRoleType identityRoleType);
    }
}