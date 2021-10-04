using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Infrastructure.Implementation.Identity
{
    /// <summary>
    /// Права пользователя
    /// </summary>
    public class UserClaimsPrincipalBoutique: UserClaimsPrincipalFactory<BoutiqueIdentityUser>
    {
        public UserClaimsPrincipalBoutique(UserManagerBoutique userManager, IOptions<IdentityOptions> optionAccessor)
            :base(userManager, optionAccessor)
        { }
    }
}