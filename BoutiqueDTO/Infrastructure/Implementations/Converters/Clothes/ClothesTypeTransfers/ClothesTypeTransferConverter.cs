using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeTransferConverter : TransferConverter<string, IClothesTypeDomain, ClothesTypeTransfer>,
                                                    IClothesTypeTransferConverter
    {
        public ClothesTypeTransferConverter(ICategoryTransferConverter categoryTransferConverter, 
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
        public override ClothesTypeTransfer ToTransfer(IClothesTypeDomain clothesTypeFullDomain) =>
            new ClothesTypeTransfer(clothesTypeFullDomain,
                                    _categoryTransferConverter.ToTransfer(clothesTypeFullDomain.Category),
                                    _genderTransferConverter.ToTransfers(clothesTypeFullDomain.Genders));

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeDomain FromTransfer(ClothesTypeTransfer clothesTypeTransfer) =>
            new ClothesTypeDomain(clothesTypeTransfer, 
                                  _categoryTransferConverter.FromTransfer(clothesTypeTransfer.Category),
                                  _genderTransferConverter.FromTransfers(clothesTypeTransfer.Genders));
    }
}