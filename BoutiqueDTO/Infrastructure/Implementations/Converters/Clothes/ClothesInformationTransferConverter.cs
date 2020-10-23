using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesInformationTransferConverter : TransferConverter<int, IClothesInformationDomain, ClothesInformationTransfer>,
                                                       IClothesInformationTransferConverter
    {
        public ClothesInformationTransferConverter(IClothesShortTransferConverter clothesShortTransferConverter,
                                                   IGenderTransferConverter genderTransferConverter,
                                                   IClothesTypeTransferConverter clothesTypeTransferConverter,
                                                   IColorClothesTransferConverter colorClothesTransferConverter,
                                                   ISizeGroupTransferConverter sizeGroupTransferConverter)
        {
            _clothesShortTransferConverter = clothesShortTransferConverter;
            _genderTransferConverter = genderTransferConverter;
            _clothesTypeTransferConverter = clothesTypeTransferConverter;
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
        private readonly IClothesTypeTransferConverter _clothesTypeTransferConverter;

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
        public override ClothesInformationTransfer ToTransfer(IClothesInformationDomain clothesInformationDomain) =>
            new ClothesInformationTransfer(_clothesShortTransferConverter.ToTransfer(clothesInformationDomain),
                                           clothesInformationDomain.Description,
                                           _genderTransferConverter.ToTransfer(clothesInformationDomain.Gender),
                                           _clothesTypeTransferConverter.ToTransfer(clothesInformationDomain.ClothesType),
                                           _colorClothesTransferConverter.ToTransfers(clothesInformationDomain.Colors),
                                           _sizeGroupTransferConverter.ToTransfers(clothesInformationDomain.SizeGroups));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesInformationDomain FromTransfer(ClothesInformationTransfer clothesInformationTransfer) =>
            new ClothesInformationDomain(_clothesShortTransferConverter.FromTransfer(clothesInformationTransfer),
                                         clothesInformationTransfer.Description,
                                         _genderTransferConverter.FromTransfer(clothesInformationTransfer.GenderTransfer),
                                         _clothesTypeTransferConverter.FromTransfer(clothesInformationTransfer.ClothesTypeTransfer),
                                         _colorClothesTransferConverter.FromTransfers(clothesInformationTransfer.Colors),
                                         _sizeGroupTransferConverter.FromTransfers(clothesInformationTransfer.SizeGroups));
    }
}