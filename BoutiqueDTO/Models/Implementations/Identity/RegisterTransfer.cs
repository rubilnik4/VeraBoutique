using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Identity
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