using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Models.Implementations.Identity
{
    public class SignInManagerBoutique : SignInManager<IdentityUser>, ISignInManagerBoutique
    {
        public SignInManagerBoutique(UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor,
                                      IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
                                      IOptions<IdentityOptions> optionAccessor, ILogger<SignInManager<IdentityUser>> logger,
                                      IAuthenticationSchemeProvider schemes, IUserConfirmation<IdentityUser> conformation)
            : base(userManager, contextAccessor, claimsFactory,
                  optionAccessor, logger, schemes, conformation)
        { }
    }
}