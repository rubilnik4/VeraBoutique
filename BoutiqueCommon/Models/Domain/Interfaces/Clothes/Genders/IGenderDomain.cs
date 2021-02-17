using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders
{
    /// <summary>
    /// Тип пола. Доменная модель
    /// </summary>
    public interface IGenderDomain: IGenderBase, IDomainModel<GenderType>
    { }
}