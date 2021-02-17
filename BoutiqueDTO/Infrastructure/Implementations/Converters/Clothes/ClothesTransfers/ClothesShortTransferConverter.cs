using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Конвертер одежды в трансферную модель
    /// </summary>
    public class ClothesShortTransferConverter : TransferConverter<int, IClothesDomain, ClothesTransfer>,
                                                 IClothesShortTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override ClothesTransfer ToTransfer(IClothesDomain clothesDomain) =>
            new ClothesTransfer(clothesDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IClothesDomain> FromTransfer(ClothesTransfer clothesTransfer) =>
            new ClothesDomain(clothesTransfer).
            Map(clothesShort => new ResultValue<IClothesDomain>(clothesShort));
    }
}