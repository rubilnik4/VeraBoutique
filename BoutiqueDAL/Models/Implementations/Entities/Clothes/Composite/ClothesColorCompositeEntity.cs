using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность одежды с цветом
    /// </summary>
    public class ClothesColorCompositeEntity : IClothesColorCompositeEntity
    {
        public ClothesColorCompositeEntity(int clothesId, string colorName)
           : this(clothesId, colorName, null, null)
        { }

        public ClothesColorCompositeEntity(int clothesId, string colorName,
                                           ClothesInformationEntity? clothesInformationEntity, 
                                           ColorClothesEntity? colorClothesEntity)
        {
            ClothesId = clothesId;
            ColorName = colorName;
            ClothesInformationEntity = clothesInformationEntity;
            ColorClothesEntity = colorClothesEntity;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (int, string) Id => (ClothesId, ColorName);

        /// <summary>
        /// Идентификатор одежды
        /// </summary>
        public int ClothesId { get; }

        /// <summary>
        /// Идентификатор цвета
        /// </summary>
        public string ColorName { get; }

        /// <summary>
        /// Одежда. Информация
        /// </summary>
        public ClothesInformationEntity? ClothesInformationEntity { get; }

        /// <summary>
        /// Цвет одежды
        /// </summary>
        public ColorClothesEntity? ColorClothesEntity { get; }
    }
}