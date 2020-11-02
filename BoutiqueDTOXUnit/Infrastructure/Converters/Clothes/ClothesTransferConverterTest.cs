using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfer;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfer;
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
        public void ClothesFull_ToTransfer_FromTransfer()
        {
            var clothesInformation = ClothesData.ClothesInformationDomains.First();
            var clothesShortTransferConverter = new ClothesShortTransferConverter();
            var clothesTypeShortTransferConverter = new ClothesTypeShortTransferConverter(new CategoryTransferConverter(),
                                                                                     new GenderTransferConverter());
            var colorClothesTransferConverter = new ColorClothesTransferConverter();
            var sizeGroupTransferConverter = new SizeGroupTransferConverter(new SizeTransferConverter());
            var clothesInformationTransferConverter = new ClothesFullTransferConverter(clothesShortTransferConverter,
                                                                                       clothesTypeShortTransferConverter,
                                                                                       colorClothesTransferConverter,
                                                                                       sizeGroupTransferConverter);

            var clothesInformationTransfer = clothesInformationTransferConverter.ToTransfer(clothesInformation);
            var clothesInformationAfterConverter = clothesInformationTransferConverter.FromTransfer(clothesInformationTransfer);

            Assert.True(clothesInformation.Equals(clothesInformationAfterConverter));
        }
    }
}