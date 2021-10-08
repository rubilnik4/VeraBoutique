using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    /// <summary>
    /// Права пользователя
    /// </summary>
    public class UserClaimsPrincipalBoutique: UserClaimsPrincipalFactory<BoutiqueUserEntity>
    {
        public UserClaimsPrincipalBoutique(UserManagerBoutique userManager, IOptions<IdentityOptions> optionAccessor)
            :base(userManager, optionAccessor)
        { }
    }
}