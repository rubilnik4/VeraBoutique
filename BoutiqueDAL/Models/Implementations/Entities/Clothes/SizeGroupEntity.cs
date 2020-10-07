using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
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
           : this(clothesSizeType, sizeNormalize, Enumerable.Empty<SizeGroupCompositeEntity>())
        { }

        public SizeGroupEntity(ClothesSizeType clothesSizeType, int sizeNormalize,
                               IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities)
            : base(clothesSizeType, sizeNormalize)
        {
            SizeGroupCompositeEntities = sizeGroupCompositeEntities.ToList();
        }

        /// <summary>
        /// Связующая сущность размера одежды
        /// </summary>
        public IReadOnlyCollection<SizeGroupCompositeEntity> SizeGroupCompositeEntities { get; }

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public override IReadOnlyCollection<SizeEntity> Sizes =>
            SizeGroupCompositeEntities?.
            Select(sizeComposite => new SizeEntity(sizeComposite.SizeType, sizeComposite.SizeName)).
            ToList()
            ?? new List<SizeEntity>();
    }
}