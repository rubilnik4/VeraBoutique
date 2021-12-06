using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ImageTransfers
{
    /// <summary>
    /// Изображение. Трансферная модель
    /// </summary>
    public interface IClothesImageTransfer : IClothesImageBase, ITransferModel<Guid>
    { }
}