using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер основной информации вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeShortTransferConverter : TransferConverter<string, IClothesTypeShortDomain, ClothesTypeShortTransfer>,
                                                     IClothesTypeShortTransferConverter
    {
        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override ClothesTypeShortTransfer ToTransfer(IClothesTypeShortDomain clothesTypeShortDomain) =>
            new ClothesTypeShortTransfer(clothesTypeShortDomain.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeShortDomain FromTransfer(ClothesTypeShortTransfer clothesTypeShortTransfer) =>
            new ClothesTypeShortDomain(clothesTypeShortTransfer.Name);
    }
}