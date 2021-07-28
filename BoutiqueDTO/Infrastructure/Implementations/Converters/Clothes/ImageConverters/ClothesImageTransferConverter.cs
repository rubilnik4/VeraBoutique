using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ImageConverters
{
    /// <summary>
    /// Конвертер изображений в трансферную модель
    /// </summary>
    public class ClothesImageTransferConverter : TransferConverter<int, IClothesImageDomain, ClothesImageTransfer>,
                                                 IClothesImageTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override ClothesImageTransfer ToTransfer(IClothesImageDomain clothesImageDomain) =>
            new ClothesImageTransfer(clothesImageDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IClothesImageDomain> FromTransfer(ClothesImageTransfer clothesImageTransfer) =>
            new ClothesImageDomain(clothesImageTransfer).
            ToResultValue();
    }
}