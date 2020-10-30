using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;
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
        public ClothesTypeTransferConverter(IGenderTransferConverter genderTransferConverter,
                                            ICategoryTransferConverter categoryTransferConverter)
        {
            _genderTransferConverter = genderTransferConverter;
            _categoryTransferConverter = categoryTransferConverter;
        }

        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        private readonly IGenderTransferConverter _genderTransferConverter;

        /// <summary>
        /// Конвертер категорий одежды в трансферную модель
        /// </summary>
        private readonly ICategoryTransferConverter _categoryTransferConverter;

        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override ClothesTypeTransfer ToTransfer(IClothesTypeDomain clothesTypeDomain) =>
            new ClothesTypeTransfer(clothesTypeDomain,
                                    _genderTransferConverter.ToTransfer(clothesTypeDomain.GenderDomain),
                                    _categoryTransferConverter.ToTransfer(clothesTypeDomain.CategoryDomain));

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeDomain FromTransfer(ClothesTypeTransfer clothesTypeTransfer) =>
            new ClothesTypeDomain(clothesTypeTransfer.Name,
                                  _genderTransferConverter.FromTransfer(clothesTypeTransfer.GenderTransfer),
                                  _categoryTransferConverter.FromTransfer(clothesTypeTransfer.CategoryTransfer));
    }
}