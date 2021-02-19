using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Группа размеров одежды. Сущность базы данных
    /// </summary>
    public class SizeGroupEntity : SizeGroupBase, ISizeGroupEntity
    {
        public SizeGroupEntity(ISizeGroupBase sizeGroup)
          : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }
        
        public SizeGroupEntity(ClothesSizeType clothesSizeType, int sizeNormalize)
            : this(clothesSizeType, sizeNormalize, null, null)
        { }

        public SizeGroupEntity(ISizeGroupBase sizeGroup, IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizeGroupCompositeEntities)
        { }

        public SizeGroupEntity(ClothesSizeType clothesSizeType, int sizeNormalize,
                               IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities)
            : this(clothesSizeType, sizeNormalize, sizeGroupCompositeEntities, null)
        { }

        public SizeGroupEntity(ClothesSizeType clothesSizeType, int sizeNormalize,
                               IEnumerable<SizeGroupCompositeEntity>? sizeGroupCompositeEntities,
                               IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupCompositeEntity)
            : base(clothesSizeType, sizeNormalize)
        {
            SizeGroupComposites = sizeGroupCompositeEntities?.ToList();
            ClothesSizeGroupComposites = clothesSizeGroupCompositeEntity?.ToList();
        }

        /// <summary>
        /// Связующая сущность размера одежды
        /// </summary>
        public IReadOnlyCollection<SizeGroupCompositeEntity>? SizeGroupComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и размера
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}