using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;

namespace BoutiqueCommon.Models.Domain.Implementations.Identities
{
    /// <summary>
    /// Имя пользователя и пароль. Доменная модель
    /// </summary>
    public class AuthorizeDomain : AuthorizeBase, IAuthorizeDomain
    {
        public AuthorizeDomain(IAuthorizeBase authorize)
            : this(authorize.Email, authorize.Password)
        { }

        public AuthorizeDomain(string email, string password)
            :base(email, password)
        { }
    }
}