using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identities
{
    /// <summary>
    /// Регистрация. Доменная модель
    /// </summary>
    public interface IRegisterDomain : IRegisterBase<IAuthorizeDomain, IPersonalDomain>, IDomainModel<string>
    { }
}