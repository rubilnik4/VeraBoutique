using BoutiqueCommon.Models.Enums.Identities;

namespace BoutiqueCommon.Models.Common.Interfaces.Identities
{
    /// <summary>
    /// Регистрация и роль
    /// </summary>
    public interface IRegisterRoleBase<TAuthorize, TPersonal> : IRegisterBase<TAuthorize, TPersonal>
        where TAuthorize : IAuthorizeBase
        where TPersonal : IPersonalBase
    {
        /// <summary>
        /// Тип роли для авторизации
        /// </summary>
        IdentityRoleType IdentityRoleType { get; }
    }
}