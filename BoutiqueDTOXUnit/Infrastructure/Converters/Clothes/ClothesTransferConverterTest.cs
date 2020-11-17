using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель. Тесты
    /// </summary>
    public class ClothesTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ClothesShort_ToTransfer_FromTransfer()
        {
            var clothesShort = ClothesData.ClothesShortDomains.First();
            var clothesShortTransferConverter = new ClothesShortTransferConverter();

            var clothesShortTransfer = clothesShortTransferConverter.ToTransfer(clothesShort);
            var clothesShortAfterConverter = clothesShortTransferConverter.FromTransfer(clothesShortTransfer);

            Assert.True(clothesShort.Equals(clothesShortAfterConverter));
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_FromTransfer()
        {
            var clothesInformation = ClothesData.ClothesDomains.First();
            var clothesShortTransferConverter = new ClothesShortTransferConverter();
            var genderTransferConverter = new GenderTransferConverter();
            var clothesTypeShortTransferConverter = new ClothesTypeShortTransferConverter(new CategoryTransferConverter());
            var colorClothesTransferConverter = new ColorClothesTransferConverter();
            var sizeGroupTransferConverter = new SizeGroupTransferConverter(new SizeTransferConverter());
            var clothesInformationTransferConverter = new ClothesTransferConverter(clothesShortTransferConverter,
                                                                                   genderTransferConverter,
                                                                                   clothesTypeShortTransferConverter,
                                                                                   colorClothesTransferConverter,
                                                                                   sizeGroupTransferConverter);

            var clothesInformationTransfer = clothesInformationTransferConverter.ToTransfer(clothesInformation);
            var clothesInformationAfterConverter = clothesInformationTransferConverter.FromTransfer(clothesInformationTransfer);

            Assert.True(clothesInformation.Equals(clothesInformationAfterConverter));
        }
    }
}