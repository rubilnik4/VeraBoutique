using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Сущность базы данных
    /// </summary>
    public class SizeGroupEntity : SizeGroup<SizeEntity>, ISizeGroupEntity
    {
        public SizeGroupEntity(ClothesSizeType clothesSizeType, int sizeNormalize)
           : this(clothesSizeType, sizeNormalize, Enumerable.Empty<SizeEntity>())
        { }

        public SizeGroupEntity(ClothesSizeType clothesSizeType, int sizeNormalize,
                               IEnumerable<SizeEntity> sizes)
            :base(clothesSizeType, sizeNormalize, sizes)
        { }
    }
}