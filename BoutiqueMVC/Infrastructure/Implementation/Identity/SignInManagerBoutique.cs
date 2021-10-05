using BoutiqueDAL.Infrastructure.Implementations.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Interfaces.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Infrastructure.Implementation.Identity
{
    public class SignInManagerBoutique : SignInManager<BoutiqueIdentityUser>, ISignInManagerBoutique
    {
        public SignInManagerBoutique(UserManagerBoutique userManager, IHttpContextAccessor contextAccessor,
                                     IUserClaimsPrincipalFactory<BoutiqueIdentityUser> claimsFactory,
                                     IOptions<IdentityOptions> optionAccessor, ILogger<SignInManager<BoutiqueIdentityUser>> logger,
                                     IAuthenticationSchemeProvider schemes, IUserConfirmation<BoutiqueIdentityUser> userConfirmation)
            : base(userManager, contextAccessor, claimsFactory, optionAccessor, logger, schemes, userConfirmation)
        { }
    }
}