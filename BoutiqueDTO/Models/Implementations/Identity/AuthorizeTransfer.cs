using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using Newtonsoft.Json;
using static BoutiqueCommon.Models.Common.Implementations.Identity.IdentitySettings;

namespace BoutiqueDTO.Models.Implementations.Identity
{
    /// <summary>
    /// Имя пользователя и пароль. Трансферная модель
    /// </summary>
    public class AuthorizeTransfer: AuthorizeBase, IAuthorizeTransfer
    {
        public AuthorizeTransfer(IAuthorizeBase authorize)
           : this(authorize.Login, authorize.Password)
        { }

        [JsonConstructor]
        public AuthorizeTransfer(string login, string password)
            :base(login, password)
        { }
    }
}