using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность размера одежды с группой
    /// </summary>
    public class SizeGroupCompositeEntity : ISizeGroupCompositeEntity
    {
        public SizeGroupCompositeEntity(int sizeId, int sizeGroupId)
            : this(sizeId, sizeGroupId, null, null)
        { }

        public SizeGroupCompositeEntity(int sizeId, int sizeGroupId, SizeEntity? size, SizeGroupEntity? sizeGroup)
        {
            SizeId = sizeId;
            SizeGroupId = sizeGroupId;
            Size = size;
            SizeGroup = sizeGroup;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (int, int) Id => (SizeId, SizeGroupId);

        /// <summary>
        /// Идентификатор размера одежды
        /// </summary>
        public int SizeId { get; }

        /// <summary>
        /// Идентификатор группы размеров одежды
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