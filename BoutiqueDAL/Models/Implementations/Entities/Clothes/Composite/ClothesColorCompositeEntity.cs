using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
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
                                           ClothesEntity? clothes, 
                                           ColorClothesEntity? colorClothes)
        {
            ClothesId = clothesId;
            ColorName = colorName;
            Clothes = clothes;
            ColorClothes = colorClothes;
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
        public ClothesEntity? Clothes { get; }

        /// <summary>
        /// Цвет одежды
        /// </summary>
        public ColorClothesEntity? ColorClothes { get; }
    }
}