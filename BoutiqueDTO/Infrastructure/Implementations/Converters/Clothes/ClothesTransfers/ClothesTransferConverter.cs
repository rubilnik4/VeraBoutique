using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesTransferConverter : TransferConverter<int, IClothesDomain, ClothesTransfer>,
                                            IClothesTransferConverter
    {
        public ClothesTransferConverter(IClothesShortTransferConverter clothesShortTransferConverter,
                                        IGenderTransferConverter genderTransferConverter,
                                        IClothesTypeShortTransferConverter clothesTypeShortTransferConverter,
                                        IColorClothesTransferConverter colorClothesTransferConverter,
                                        ISizeGroupTransferConverter sizeGroupTransferConverter)
        {
            _clothesShortTransferConverter = clothesShortTransferConverter;
            _genderTransferConverter = genderTransferConverter;
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
        private readonly IGenderTransferConverter _genderTransferConverter;

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
        public override ClothesTransfer ToTransfer(IClothesDomain clothesDomain) =>
            new ClothesTransfer(_clothesShortTransferConverter.ToTransfer(clothesDomain),
                                clothesDomain.Description,
                                _genderTransferConverter.ToTransfer(clothesDomain.Gender),
                                _clothesTypeShortTransferConverter.ToTransfer(clothesDomain.ClothesTypeShort),
                                _colorClothesTransferConverter.ToTransfers(clothesDomain.Colors),
                                _sizeGroupTransferConverter.ToTransfers(clothesDomain.SizeGroups));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesDomain FromTransfer(ClothesTransfer clothesTransfer) =>
            new ClothesDomain(_clothesShortTransferConverter.FromTransfer(clothesTransfer),
                              clothesTransfer.Description,
                              _genderTransferConverter.FromTransfer(clothesTransfer.Gender),
                              _clothesTypeShortTransferConverter.FromTransfer(clothesTransfer.ClothesTypeShort),
                              _colorClothesTransferConverter.FromTransfers(clothesTransfer.Colors),
                              _sizeGroupTransferConverter.FromTransfers(clothesTransfer.SizeGroups));
    }
}