using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identities
{
    /// <summary>
    /// Имя пользователя и пароль. Доменная модель
    /// </summary>
    public interface IAuthorizeDomain: IAuthorizeBase, IDomainModel<string>
    { }
}