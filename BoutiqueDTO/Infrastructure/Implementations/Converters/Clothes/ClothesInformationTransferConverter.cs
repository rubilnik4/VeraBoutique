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
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override ClothesInformationTransfer ToTransfer(IClothesInformationDomain clothesInformationDomain) =>
            new ClothesInformationTransfer(clothesInformationDomain.Id, clothesInformationDomain.Name,
                                           clothesInformationDomain.Description, clothesInformationDomain.Color,
                                           clothesInformationDomain.Sizes,
                                           clothesInformationDomain.Price, clothesInformationDomain.Image);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IClothesInformationDomain FromTransfer(ClothesInformationTransfer clothesInformationTransfer) =>
            new ClothesInformationDomain(clothesInformationTransfer.Id, clothesInformationTransfer.Name,
                                         clothesInformationTransfer.Description, clothesInformationTransfer.Color,
                                         clothesInformationTransfer.Sizes,
                                         clothesInformationTransfer.Price, clothesInformationTransfer.Image);
    }
}