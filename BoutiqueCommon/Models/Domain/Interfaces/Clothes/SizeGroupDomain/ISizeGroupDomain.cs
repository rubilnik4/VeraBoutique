using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные
    /// </summary>
    public interface ISizeGroupDomain : ISizeGroupBase, IDomainModel<int>
    { }
}