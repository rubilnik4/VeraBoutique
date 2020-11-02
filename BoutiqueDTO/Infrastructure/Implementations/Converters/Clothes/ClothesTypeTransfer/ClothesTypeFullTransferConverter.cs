using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfer;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfer
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeFullTransferConverter : TransferConverter<string, IClothesTypeFullDomain, ClothesTypeFullTransfer>,
                                                    IClothesTypeFullTransferConverter
    {
        public ClothesTypeFullTransferConverter(ICategoryTransferConverter categoryTransferConverter, 
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
        public override ClothesTypeFullTransfer ToTransfer(IClothesTypeFullDomain clothesTypeFullDomain) =>
            new ClothesTypeFullTransfer(clothesTypeFullDomain,
                                        _categoryTransferConverter.ToTransfer(clothesTypeFullDomain.Category),
                                        _genderTransferConverter.ToTransfers(clothesTypeFullDomain.Genders));

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IClothesTypeFullDomain FromTransfer(ClothesTypeFullTransfer clothesTypeFullTransfer) =>
            new ClothesTypeFullDomain(clothesTypeFullTransfer, 
                                      _categoryTransferConverter.FromTransfer(clothesTypeFullTransfer.Category),
                                      _genderTransferConverter.FromTransfers(clothesTypeFullTransfer.Genders));
    }
}