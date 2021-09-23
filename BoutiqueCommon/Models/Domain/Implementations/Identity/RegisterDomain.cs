using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommon.Models.Domain.Implementations.Identity
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