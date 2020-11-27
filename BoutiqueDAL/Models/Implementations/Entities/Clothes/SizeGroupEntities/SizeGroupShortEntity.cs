using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные. Сущность базы данных
    /// </summary>
    public class SizeGroupShortEntity : SizeGroup, ISizeGroupShortEntity
    {
        public SizeGroupShortEntity(ISizeGroup sizeGroup)
            :this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }

        public SizeGroupShortEntity(ClothesSizeType clothesSizeType, int sizeNormalize)
              : base(clothesSizeType, sizeNormalize)
        { }
    }
}