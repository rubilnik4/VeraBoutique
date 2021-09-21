using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identity
{
    /// <summary>
    /// Регистрация. Доменная модель
    /// </summary>
    public interface IRegisterDomain : IRegisterBase, IDomainModel<string>
    { }
}