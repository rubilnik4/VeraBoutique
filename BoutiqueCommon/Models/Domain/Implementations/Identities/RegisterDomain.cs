using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;

namespace BoutiqueCommon.Models.Domain.Implementations.Identities
{
    /// <summary>
    /// Регистрация. Доменная модель
    /// </summary>
    public class RegisterDomain : RegisterBase<IAuthorizeDomain, IPersonalDomain>, IRegisterDomain
    {
        public RegisterDomain(IRegisterBase<IAuthorizeDomain, IPersonalDomain> register)
            : this(register.Authorize, register.Personal)
        { }

        public RegisterDomain(IAuthorizeDomain authorize, IPersonalDomain personal)
            : base(authorize, personal)
        { }
    }
}