using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{

    /// <summary>
    /// Связующая сущность одежды с размером
    /// </summary>
    public class ClothesSizeGroupCompositeEntity : IClothesSizeCompositeEntity
    {
        public ClothesSizeGroupCompositeEntity(int clothesId, int sizeGroupId)
            : this(clothesId, sizeGroupId, null, null)
            { }

        public ClothesSizeGroupCompositeEntity(int clothesId, int sizeGroupId,
                                               ClothesEntity? clothes, SizeGroupEntity? sizeGroup)
        {
            ClothesId = clothesId;
            SizeGroupId = sizeGroupId;
            Clothes = clothes;
            SizeGroup = sizeGroup;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (int, int) Id => (ClothesId, SizeGroupId);

        /// <summary>
        /// Идентификатор одежды
        /// </summary>
        public int ClothesId { get; }

        /// <summary>
        /// Идентификатор размера одежды
        /// </summary>
        public int SizeGroupId { get; }

        /// <summary>
        /// Одежда. Информация
        /// </summary>
        public ClothesEntity? Clothes { get; }

        /// <summary>
        /// Группа размеров одежды
        /// </summary>
        public SizeGroupEntity? SizeGroup { get; }
    }
}