using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers
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
        public override ClothesTypeShortTransfer ToTransfer(IClothesTypeShortDomain clothesTypeDomain) =>
            new ClothesTypeShortTransfer(clothesTypeDomain);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IResultValue<IClothesTypeShortDomain> FromTransfer(ClothesTypeShortTransfer clothesTypeTransfer) =>
            new ClothesTypeShortDomain(clothesTypeTransfer).
            Map(clothesTypeShort => new ResultValue<IClothesTypeShortDomain>(clothesTypeShort));
    }
}