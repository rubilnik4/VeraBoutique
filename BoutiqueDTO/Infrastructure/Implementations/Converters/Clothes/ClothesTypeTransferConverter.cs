using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeTransferConverter : TransferConverter<string, IClothesTypeDomain, ClothesTypeTransfer>,
                                                IClothesTypeTransferConverter
    {
        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override ClothesTypeTransfer ToTransfer(IClothesTypeDomain clothesTypeDomain) =>
            new ClothesTypeTransfer(clothesTypeDomain.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeDomain FromTransfer(ClothesTypeTransfer clothesTypeTransfer) =>
            new ClothesTypeDomain(clothesTypeTransfer.Name);
    }
}