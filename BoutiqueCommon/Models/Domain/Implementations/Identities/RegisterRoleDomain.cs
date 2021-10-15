using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Domain.Implementations.Identities
{
    /// <summary>
    /// Регистрация и роль. Доменная модель
    /// </summary>
    public class RegisterRoleDomain : RegisterRoleBase<IAuthorizeDomain, IPersonalDomain>, IRegisterRoleDomain
    {
        public RegisterRoleDomain(IRegisterDomain register, IdentityRoleType identityRoleType)
            : this(register.Authorize, register.Personal, identityRoleType)
        { }

        public RegisterRoleDomain(IAuthorizeDomain authorize, IPersonalDomain personal, IdentityRoleType identityRoleType)
            : base(authorize, personal, identityRoleType)
        { }

        /// <summary>
        /// Преобразовать в пользователя
        /// </summary>
        public IBoutiqueUserDomain ToBoutiqueUser() =>
            new BoutiqueUserDomain(Authorize.Email, Authorize.Email, IdentityRoleType,
                                   Personal.Name, Personal.Surname, Personal.Address, Personal.Phone);
    }
}