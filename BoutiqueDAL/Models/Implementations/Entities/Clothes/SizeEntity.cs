using System;
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
        public SizeEntity(SizeType sizeType, int sizeValue)
         : this(sizeType, sizeValue, sizeValue.ToString(), null, null, null)
        { }

        public SizeEntity(SizeType sizeType, int sizeValue, string sizeName)
            : this(sizeType, sizeValue, sizeName, null, null, null)
        { }

        public SizeEntity(SizeType sizeType, int sizeValue,
                         (ClothesSizeType ClothesSizeType, int sizeNormalize) sizeGroupEntityId)
           : this(sizeType, sizeValue, sizeValue.ToString(),
                  sizeGroupEntityId.ClothesSizeType, sizeGroupEntityId.sizeNormalize, null)
        { }

        public SizeEntity(SizeType sizeType, int sizeValue, string sizeName,
                          (ClothesSizeType ClothesSizeType, int sizeNormalize) sizeGroupEntityId)
            : this(sizeType, sizeValue, sizeName,
                   sizeGroupEntityId.ClothesSizeType, sizeGroupEntityId.sizeNormalize, null)
        { }

        public SizeEntity(SizeType sizeType, int sizeValue, string sizeName,
                          ClothesSizeType? clothesSizeTypeId, int? sizeNormalizeId, SizeGroupEntity? sizeGroupEntity)
            : base(sizeType, sizeValue, sizeName)
        {
            ClothesSizeTypeId = clothesSizeTypeId;
            SizeNormalizeId = sizeNormalizeId;
            SizeGroupEntity = sizeGroupEntity;
        }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды для размеров
        /// </summary>
        public ClothesSizeType? ClothesSizeTypeId { get; }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды для размеров
        /// </summary>
        public int? SizeNormalizeId { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public SizeGroupEntity? SizeGroupEntity { get; }
    }
}