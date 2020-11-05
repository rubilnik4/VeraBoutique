using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{

    /// <summary>
    /// Связующая сущность одежды с размером
    /// </summary>
    public class ClothesSizeGroupCompositeEntity : IClothesSizeCompositeEntity
    {
        public ClothesSizeGroupCompositeEntity(int clothesId, ClothesSizeType clothesSizeType, int sizeNormalize)
        : this(clothesId, clothesSizeType, sizeNormalize, null, null)
        { }

        public ClothesSizeGroupCompositeEntity(int clothesId, ClothesSizeType clothesSizeType, int sizeNormalize,
                                               ClothesEntity? clothesInformationEntity,
                                               SizeGroupEntity? sizeGroupEntity)
        {
            ClothesId = clothesId;
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
            Clothes = clothesInformationEntity;
            SizeGroup = sizeGroupEntity;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (int, (ClothesSizeType, int)) Id => (ClothesId, (ClothesSizeType, SizeNormalize));

        /// <summary>
        /// Идентификатор одежды
        /// </summary>
        public int ClothesId { get; }

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        public int SizeNormalize { get; }

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