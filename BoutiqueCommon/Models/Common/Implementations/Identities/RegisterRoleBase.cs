using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Common.Implementations.Identities
{
    /// <summary>
    /// Регистрация и роль
    /// </summary>
    public abstract class RegisterRoleBase<TAuthorize, TPersonal> : RegisterBase<TAuthorize, TPersonal>,
                                                                    IRegisterRoleBase<TAuthorize, TPersonal>
        where TAuthorize : IAuthorizeBase
        where TPersonal : IPersonalBase
    {
        protected RegisterRoleBase(TAuthorize authorize, TPersonal personal, IdentityRoleType identityRoleType)
            : base(authorize, personal)
        {
            IdentityRoleType = identityRoleType;
        }

        /// <summary>
        /// Тип роли для авторизации
        /// </summary>
        public IdentityRoleType IdentityRoleType { get; }
    }
}