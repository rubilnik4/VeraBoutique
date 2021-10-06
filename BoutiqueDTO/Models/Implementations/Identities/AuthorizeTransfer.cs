using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueDTO.Models.Interfaces.Identities;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identities
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