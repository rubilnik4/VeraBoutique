using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Models.Implementations.Identity
{
    public class SignInManagerBoutique : SignInManager<BoutiqueUser>, ISignInManagerBoutique
    {
        public SignInManagerBoutique(UserManagerBoutique userManager, IHttpContextAccessor contextAccessor,
                                      IUserClaimsPrincipalFactory<BoutiqueUser> claimsFactory,
                                      IOptions<IdentityOptions> optionAccessor, ILogger<SignInManager<BoutiqueUser>> logger,
                                      IAuthenticationSchemeProvider schemes, IUserConfirmation<BoutiqueUser> conformation)
            : base(userManager, contextAccessor, claimsFactory,
                  optionAccessor, logger, schemes, conformation)
        { }
    }
}