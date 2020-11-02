using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfer;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfer
{
    /// <summary>
    /// Конвертер основной информации вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeShortTransferConverter : TransferConverter<string, IClothesTypeShortDomain, ClothesTypeShortTransfer>,
                                                     IClothesTypeShortTransferConverter
    {
        public ClothesTypeShortTransferConverter(ICategoryTransferConverter categoryTransferConverter,
                                                 IGenderTransferConverter genderTransferConverter)
        {
            _categoryTransferConverter = categoryTransferConverter;
            _genderTransferConverter = genderTransferConverter;
        }

        /// <summary>
        /// Конвертер категорий одежды в трансферную модель
        /// </summary>
        private readonly ICategoryTransferConverter _categoryTransferConverter;

        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        private readonly IGenderTransferConverter _genderTransferConverter;

        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override ClothesTypeShortTransfer ToTransfer(IClothesTypeShortDomain clothesTypeShortDomain) =>
            new ClothesTypeShortTransfer(clothesTypeShortDomain,
                                         _categoryTransferConverter.ToTransfer(clothesTypeShortDomain.Category),
                                         _genderTransferConverter.ToTransfer(clothesTypeShortDomain.Gender));

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeShortDomain FromTransfer(ClothesTypeShortTransfer clothesTypeShortTransfer) =>
            new ClothesTypeShortDomain(clothesTypeShortTransfer, 
                                       _categoryTransferConverter.FromTransfer(clothesTypeShortTransfer.Category),
                                       _genderTransferConverter.FromTransfer(clothesTypeShortTransfer.Gender));
    }
}