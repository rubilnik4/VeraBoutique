using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Models.Implementations.Identity
{
    /// <summary>
    /// Права пользователя
    /// </summary>
    public class UserClaimsPrincipalBoutique: UserClaimsPrincipalFactory<BoutiqueUser>
    {
        public UserClaimsPrincipalBoutique(UserManagerBoutique userManager, IOptions<IdentityOptions> optionAccessor)
            :base(userManager, optionAccessor)
        { }
    }
}