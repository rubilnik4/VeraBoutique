using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные. Сущность базы данных
    /// </summary>
    public class SizeGroupShortEntity : SizeGroupShortBase, ISizeGroupShortEntity
    {
        public SizeGroupShortEntity(ISizeGroupShortBase sizeGroupShort)
            : this(sizeGroupShort.ClothesSizeType, sizeGroupShort.SizeNormalize)
        { }

        public SizeGroupShortEntity(ClothesSizeType clothesSizeType, int sizeNormalize)
            : base(clothesSizeType, sizeNormalize)
        {
            Id = GetIdHashCode(clothesSizeType, sizeNormalize);
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id { get; }
    }
}