using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные
    /// </summary>
    public interface ISizeGroupShortDomain : ISizeGroup, IDomainModel<(ClothesSizeType, int)>, IEquatable<ISizeGroupShortDomain>
    { }
}