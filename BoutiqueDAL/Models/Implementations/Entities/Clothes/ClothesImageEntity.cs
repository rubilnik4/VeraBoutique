using BoutiqueCommon.Models.Common.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Owns;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Сущность изображения
    /// </summary>
    public class ClothesImageEntity : ClothesImageBase, IClothesImageEntity
    {
        public ClothesImageEntity(IClothesImageBase clothesImage)
          : this(clothesImage.Id, clothesImage.Image, clothesImage.IsMain, clothesImage.ClothesId, null)
        { }

        public ClothesImageEntity(IClothesImageBase clothesImage, ClothesEntity? clothes)
           : this(clothesImage.Id, clothesImage.Image, clothesImage.IsMain, clothesImage.ClothesId, clothes)
        { }

        public ClothesImageEntity(int id, byte[] image, bool isMain, int clothesId)
          : this(id, image, isMain, clothesId, null)
        { }

        public ClothesImageEntity(int id, byte[] image, bool isMain, int clothesId, ClothesEntity? clothes)
            : base(id, image, isMain, clothesId)
        {
            Clothes = clothes;
        }

        /// <summary>
        /// Сущность одежды
        /// </summary>
        public ClothesEntity? Clothes { get; }
    }
}