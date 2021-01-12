using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identity
{
    /// <summary>
    /// Имя пользователя и пароль. Доменная модель
    /// </summary>
    public interface IAuthorizeDomain: IAuthorizeBase, IDomainModel<(string, string)>
    { }
}