using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueDTO.Models.Interfaces.Identities;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identities
{
    /// <summary>
    /// Регистрация. Трансферная модель
    /// </summary>
    public class RegisterTransfer : RegisterBase<AuthorizeTransfer, PersonalTransfer>, IRegisterTransfer
    {
        [JsonConstructor]
        public RegisterTransfer(AuthorizeTransfer authorize, PersonalTransfer personal)
            : base(authorize, personal)
        { }
    }
}