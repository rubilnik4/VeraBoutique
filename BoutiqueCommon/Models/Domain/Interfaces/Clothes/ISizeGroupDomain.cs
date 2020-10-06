using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public interface ISizeGroupDomain : ISizeGroup, IDomainModel<(ClothesSizeType, int)>
    { }
}