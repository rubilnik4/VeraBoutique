using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные
    /// </summary>
    public class SizeGroupShortDomain : SizeGroup, ISizeGroupShortDomain
    {
        public SizeGroupShortDomain(ISizeGroup sizeGroup)
       : base(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }

        public SizeGroupShortDomain(ClothesSizeType clothesSizeType, int sizeNormalize)
         : base(clothesSizeType, sizeNormalize)
        { }
    }
}