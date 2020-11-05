using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfer;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfer
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesTransferConverter : TransferConverter<int, IClothesDomain, Models.Implementations.Clothes.ClothesTransfers.ClothesTransfer>,
                                            IClothesTransferConverter
    {
        public ClothesTransferConverter(IClothesShortTransferConverter clothesShortTransferConverter,
                                        IClothesTypeShortTransferConverter clothesTypeShortTransferConverter,
                                        IColorClothesTransferConverter colorClothesTransferConverter,
                                        ISizeGroupTransferConverter sizeGroupTransferConverter)
        {
            _clothesShortTransferConverter = clothesShortTransferConverter;
            _clothesTypeShortTransferConverter = clothesTypeShortTransferConverter;
            _colorClothesTransferConverter = colorClothesTransferConverter;
            _sizeGroupTransferConverter = sizeGroupTransferConverter;
        }

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesShortTransferConverter _clothesShortTransferConverter;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTypeShortTransferConverter _clothesTypeShortTransferConverter;

        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        private readonly IColorClothesTransferConverter _colorClothesTransferConverter;

        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        private readonly ISizeGroupTransferConverter _sizeGroupTransferConverter;

        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override Models.Implementations.Clothes.ClothesTransfers.ClothesTransfer ToTransfer(IClothesDomain clothesDomain) =>
            new Models.Implementations.Clothes.ClothesTransfers.ClothesTransfer(_clothesShortTransferConverter.ToTransfer(clothesDomain),
                                                                                clothesDomain.Description,
                                                                                _clothesTypeShortTransferConverter.ToTransfer(clothesDomain.ClothesTypeShort),
                                                                                _colorClothesTransferConverter.ToTransfers(clothesDomain.Colors),
                                                                                _sizeGroupTransferConverter.ToTransfers(clothesDomain.SizeGroups));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesDomain FromTransfer(Models.Implementations.Clothes.ClothesTransfers.ClothesTransfer clothesTransfer) =>
            new ClothesDomain(_clothesShortTransferConverter.FromTransfer(clothesTransfer),
                                  clothesTransfer.Description,
                                  _clothesTypeShortTransferConverter.FromTransfer(clothesTransfer.ClothesTypeShort),
                                  _colorClothesTransferConverter.FromTransfers(clothesTransfer.Colors),
                                  _sizeGroupTransferConverter.FromTransfers(clothesTransfer.SizeGroups));
    }
}