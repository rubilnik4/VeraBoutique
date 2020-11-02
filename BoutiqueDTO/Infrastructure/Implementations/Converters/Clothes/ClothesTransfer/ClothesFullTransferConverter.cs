using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfer;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfer;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfer
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesFullTransferConverter : TransferConverter<int, IClothesFullDomain, ClothesFullTransfer>,
                                            IClothesFullTransferConverter
    {
        public ClothesFullTransferConverter(IClothesShortTransferConverter clothesShortTransferConverter,
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
        public override ClothesFullTransfer ToTransfer(IClothesFullDomain clothesFullDomain) =>
            new ClothesFullTransfer(_clothesShortTransferConverter.ToTransfer(clothesFullDomain),
                                    clothesFullDomain.Description,
                                    _clothesTypeShortTransferConverter.ToTransfer(clothesFullDomain.ClothesTypeShort),
                                    _colorClothesTransferConverter.ToTransfers(clothesFullDomain.Colors),
                                    _sizeGroupTransferConverter.ToTransfers(clothesFullDomain.SizeGroups));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesFullDomain FromTransfer(ClothesFullTransfer clothesFullTransfer) =>
            new ClothesFullDomain(_clothesShortTransferConverter.FromTransfer(clothesFullTransfer),
                                  clothesFullTransfer.Description,
                                  _clothesTypeShortTransferConverter.FromTransfer(clothesFullTransfer.ClothesTypeShort),
                                  _colorClothesTransferConverter.FromTransfers(clothesFullTransfer.Colors),
                                  _sizeGroupTransferConverter.FromTransfers(clothesFullTransfer.SizeGroups));
    }
}