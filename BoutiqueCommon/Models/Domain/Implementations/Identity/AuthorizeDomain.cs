using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommon.Models.Domain.Implementations.Identity
{
    /// <summary>
    /// Имя пользователя и пароль. Доменная модель
    /// </summary>
    public class AuthorizeDomain : AuthorizeBase, IAuthorizeDomain
    {
        public AuthorizeDomain(IAuthorizeBase authorize)
            : this(authorize.UserName, authorize.Password)
        { }

        public AuthorizeDomain(string userName, string password)
            :base(userName, password)
        { }
    }
}