using BoutiqueCommon.Models.Common.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.Images
{
    /// <summary>
    /// Изображение. Доменная модель
    /// </summary>
    public class ClothesImageDomain : ClothesImageBase, IClothesImageDomain
    {
        public ClothesImageDomain(IClothesImageBase clothesImage)
            : this(clothesImage.Id, clothesImage.Image, clothesImage.IsMain, clothesImage.ClothesId)
        { }

        public ClothesImageDomain(int id, byte[] image, bool isMain, int clothesId)
            : base(id, image, isMain, clothesId)
        { }
    }
}