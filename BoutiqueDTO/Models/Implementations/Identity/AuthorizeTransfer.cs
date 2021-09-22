using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identity
{
    /// <summary>
    /// Имя пользователя и пароль. Трансферная модель
    /// </summary>
    public class AuthorizeTransfer: AuthorizeBase, IAuthorizeTransfer
    {
        public AuthorizeTransfer(IAuthorizeBase authorize)
           : this(authorize.Email, authorize.Password)
        { }

        [JsonConstructor]
        public AuthorizeTransfer(string email, string password)
            :base(email, password)
        { }
    }
}