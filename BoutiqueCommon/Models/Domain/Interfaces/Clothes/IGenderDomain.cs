using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Тип пола. Доменная модель
    /// </summary>
    public interface IGenderDomain: IGenderBase, IDomainModel<GenderType>
    { }
}