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
        public ClothesInformationTransferConverter(IColorClothesTransferConverter colorClothesTransferConverter,
                                                   ISizeGroupTransferConverter sizeGroupTransferConverter)
        {
            _colorClothesTransferConverter = colorClothesTransferConverter;
            _sizeGroupTransferConverter = sizeGroupTransferConverter;
        }

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
            new ClothesInformationTransfer(clothesInformationDomain.Id, clothesInformationDomain.Name,
                                           clothesInformationDomain.Description,
                                           _colorClothesTransferConverter.ToTransfers(clothesInformationDomain.Colors),
                                           _sizeGroupTransferConverter.ToTransfers(clothesInformationDomain.SizeGroups),
                                           clothesInformationDomain.Price, clothesInformationDomain.Image);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesInformationDomain FromTransfer(ClothesInformationTransfer clothesInformationTransfer) =>
            new ClothesInformationDomain(clothesInformationTransfer.Id, clothesInformationTransfer.Name,
                                         clothesInformationTransfer.Description,
                                         _colorClothesTransferConverter.FromTransfers(clothesInformationTransfer.Colors),
                                         _sizeGroupTransferConverter.FromTransfers(clothesInformationTransfer.SizeGroups),
                                         clothesInformationTransfer.Price, clothesInformationTransfer.Image);
    }
}