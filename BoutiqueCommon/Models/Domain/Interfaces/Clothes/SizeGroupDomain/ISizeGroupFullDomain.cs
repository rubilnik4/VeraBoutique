using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public interface ISizeGroupFullDomain: ISizeGroupFullBase<ISizeDomain>, ISizeGroupDomain
    { }
}