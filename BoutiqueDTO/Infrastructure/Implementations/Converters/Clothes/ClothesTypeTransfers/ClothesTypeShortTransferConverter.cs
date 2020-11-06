using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер основной информации вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeShortTransferConverter : TransferConverter<string, IClothesTypeShortDomain, ClothesTypeShortTransfer>,
                                                     IClothesTypeShortTransferConverter
    {
        public ClothesTypeShortTransferConverter(ICategoryTransferConverter categoryTransferConverter)
        {
            _categoryTransferConverter = categoryTransferConverter;
        }

        /// <summary>
        /// Конвертер категорий одежды в трансферную модель
        /// </summary>
        private readonly ICategoryTransferConverter _categoryTransferConverter;

        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override ClothesTypeShortTransfer ToTransfer(IClothesTypeShortDomain clothesTypeShortDomain) =>
            new ClothesTypeShortTransfer(clothesTypeShortDomain,
                                         _categoryTransferConverter.ToTransfer(clothesTypeShortDomain.Category));

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeShortDomain FromTransfer(ClothesTypeShortTransfer clothesTypeShortTransfer) =>
            new ClothesTypeShortDomain(clothesTypeShortTransfer, 
                                       _categoryTransferConverter.FromTransfer(clothesTypeShortTransfer.Category));
    }
}