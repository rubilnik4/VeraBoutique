using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Сущность базы данных
    /// </summary>
    public class ClothesSizeGroupEntity : SizeGroup, IClothesSizeGroupEntity
    {
        public ClothesSizeGroupEntity(ISize clothesSizeBase,
                                      IReadOnlyCollection<ISize> clothesSizesAdditional)
            :base(clothesSizeBase, clothesSizesAdditional)
        {

        }
    }
}