using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды с размером по умолчанию
    /// </summary>
    public interface ISizeGroupDefaultDomain: ISizeGroupDefaultBase<ISizeDomain>, ISizeGroupMainDomain
    { }
}