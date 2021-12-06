using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters
{
    /// <summary>
    /// Конвертер изображений в трансферную модель
    /// </summary>
    public interface IClothesImageTransferConverter : ITransferConverter<Guid, IClothesImageDomain, ClothesImageTransfer>
    { }
}