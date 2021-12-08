using System.Collections.Generic;
using System.Security.Claims;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueMVCXUnit.Data
{
    /// <summary>
    /// Пользователь в контроллере
    /// </summary>
    public static class ClaimsData
    {
        /// <summary>
        /// Пользователь в контроллере
        /// </summary>
        public static ClaimsPrincipal GetClaimsIdentity(string email) =>
             new Claim(ClaimTypes.NameIdentifier, email).
             Map(claim => new List<Claim> { claim }).
             Map(claims => new ClaimsIdentity(claims)).
             Map(claimIdentity => new ClaimsPrincipal(claimIdentity));
    }
}