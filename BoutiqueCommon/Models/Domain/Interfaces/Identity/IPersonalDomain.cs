using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identity
{
    /// <summary>
    /// Личная информация. Доменная модель
    /// </summary>
    public interface IPersonalDomain : IPersonalBase, IDomainModel<string>
    { }
}