using BoutiqueCommon.Models.Common.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueDTO.Models.Interfaces.Clothes.ImageTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers
{
    /// <summary>
    /// Изображение. Трансферная модель
    /// </summary>
    public class ClothesImageTransfer : ClothesImageBase, IClothesImageTransfer
    {
        public ClothesImageTransfer(IClothesImageBase clothesImage)
          : this(clothesImage.Id, clothesImage.Image, clothesImage.IsMain)
        { }

        [JsonConstructor]
        public ClothesImageTransfer(int id, byte[] image, bool isMain)
            : base(id, image, isMain)
        { }
    }
}