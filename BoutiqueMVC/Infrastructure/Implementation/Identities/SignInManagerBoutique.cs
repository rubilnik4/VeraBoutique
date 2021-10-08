using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueMVC.Infrastructure.Interfaces.Identities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    public class SignInManagerBoutique : SignInManager<BoutiqueUserEntity>, ISignInManagerBoutique
    {
        public SignInManagerBoutique(UserManagerBoutique userManager, IHttpContextAccessor contextAccessor,
                                     IUserClaimsPrincipalFactory<BoutiqueUserEntity> claimsFactory,
                                     IOptions<IdentityOptions> optionAccessor, ILogger<SignInManager<BoutiqueUserEntity>> logger,
                                     IAuthenticationSchemeProvider schemes, IUserConfirmation<BoutiqueUserEntity> userConfirmation)
            : base(userManager, contextAccessor, claimsFactory, optionAccessor, logger, schemes, userConfirmation)
        { }
    }
}