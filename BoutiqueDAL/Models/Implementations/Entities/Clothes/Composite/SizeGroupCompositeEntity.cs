using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность размера одежды с группой
    /// </summary>
    public class SizeGroupCompositeEntity: ISizeGroupCompositeEntity
    {
        public SizeGroupCompositeEntity(SizeType sizeType, string sizeName,
                                        ClothesSizeType clothesSizeType, int sizeNormalize)
            : this(sizeType, sizeName, clothesSizeType, sizeNormalize, null, null)
        { }

        public SizeGroupCompositeEntity(SizeType sizeType, string sizeName, 
                                        ClothesSizeType clothesSizeType, int sizeNormalize, 
                                        SizeEntity? sizeEntity, SizeGroupEntity? sizeGroupEntity)
        {
            SizeType = sizeType;
            SizeName = sizeName;
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
            Size = sizeEntity;
            SizeGroup = sizeGroupEntity;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public ((SizeType, string), (ClothesSizeType, int)) Id => ((SizeType, SizeName), (ClothesSizeType, SizeNormalize));

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        public SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public string SizeName { get; }

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        public int SizeNormalize { get; }

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