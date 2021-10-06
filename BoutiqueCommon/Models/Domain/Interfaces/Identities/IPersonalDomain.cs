using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Identities
{
    /// <summary>
    /// Личная информация. Доменная модель
    /// </summary>
    public interface IPersonalDomain : IPersonalBase, IDomainModel<string>
    { }
}