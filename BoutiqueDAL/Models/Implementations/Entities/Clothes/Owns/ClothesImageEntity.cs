using BoutiqueCommon.Models.Common.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Owns;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns
{
    /// <summary>
    /// Сущность изображения
    /// </summary>
    [Owned]
    public class ClothesImageEntity : ClothesImageBase, IClothesImageEntity
    {
        public ClothesImageEntity(IClothesImageBase clothesImage)
           : this(clothesImage.Id, clothesImage.Image, clothesImage.IsMain)
        { }

        public ClothesImageEntity(int id, byte[] image, bool isMain)
            : base(id, image, isMain)
        { }
    }
}