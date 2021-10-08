using BoutiqueCommon.Models.Common.Interfaces.Identities;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identities
{
    /// <summary>
    /// Регистрация и роль. Доменная модель
    /// </summary>
    public interface IRegisterRoleDomain: IRegisterRoleBase<IAuthorizeDomain, IPersonalDomain>, IRegisterDomain
    {
        
    }
}