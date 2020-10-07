using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Размер одежды. Сущность базы данных
    /// </summary>
    public class SizeEntity : Size, ISizeEntity
    {

        public SizeEntity(SizeType sizeType,  string sizeName)
            : this(sizeType, sizeName, Enumerable.Empty<SizeGroupCompositeEntity>())
        { }

        public SizeEntity(SizeType sizeType, string sizeName, 
                          IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities)
            : base(sizeType,  sizeName)
        {
            SizeGroupCompositeEntities = sizeGroupCompositeEntities.ToList();
        }

        /// <summary>
        /// Связующая сущность размера одежды
        /// </summary>
        public IReadOnlyCollection<SizeGroupCompositeEntity> SizeGroupCompositeEntities { get; }
    }
}