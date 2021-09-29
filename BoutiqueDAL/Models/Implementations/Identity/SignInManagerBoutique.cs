using BoutiqueDAL.Models.Interfaces.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    public class SignInManagerBoutique : SignInManager<BoutiqueUser>, ISignInManagerBoutique
    {
        public SignInManagerBoutique(UserManagerBoutique userManager, IHttpContextAccessor contextAccessor,
                                      IUserClaimsPrincipalFactory<BoutiqueUser> claimsFactory,
                                      IOptions<IdentityOptions> optionAccessor, ILogger<SignInManager<BoutiqueUser>> logger,
                                      IAuthenticationSchemeProvider schemes)
            : base(userManager, contextAccessor, claimsFactory,
                  optionAccessor, logger, schemes)
        { }
    }
}