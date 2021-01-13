using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность размера одежды с группой
    /// </summary>
    public class SizeGroupCompositeEntity: ISizeGroupCompositeEntity
    {
        public SizeGroupCompositeEntity(SizeType sizeType, string sizeName, int sizeGroupId)
            : this(sizeType, sizeName, sizeGroupId, null, null)
        { }

        public SizeGroupCompositeEntity(SizeType sizeType, string sizeName, int sizeGroupId, 
                                        SizeEntity? size, SizeGroupEntity? sizeGroup)
        {
            SizeType = sizeType;
            SizeName = sizeName;
            SizeGroupId = sizeGroupId;
            Size = size;
            SizeGroup = sizeGroup;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public ((SizeType, string), int) Id => ((SizeType, SizeName), SizeGroupId);

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string SizeName { get; }

        /// <summary>
        /// Идентификатор размера одежды
        /// </summary>
        public int SizeGroupId { get; }

        /// <summary>
        /// Размер одежды
        /// </summary>
        public SizeEntity? Size { get; }

        /// <summary>
        /// Группа размеров одежды
        /// </summary>
        public SizeGroupEntity? SizeGroup { get; }
    }
}