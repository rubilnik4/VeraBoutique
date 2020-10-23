using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель. Тесты
    /// </summary>
    public class ClothesInformationTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var clothesInformation = ClothesData.ClothesInformationDomains.First();
            var clothesShortTransferConverter = new ClothesShortTransferConverter();
            var genderTransferConverter = new GenderTransferConverter();
            var clothesTypeTransferConverter = new ClothesTypeTransferConverter(new CategoryTransferConverter());
            var colorClothesTransferConverter = new ColorClothesTransferConverter();
            var sizeGroupTransferConverter = new SizeGroupTransferConverter(new SizeTransferConverter());
            var clothesInformationTransferConverter = new ClothesInformationTransferConverter(clothesShortTransferConverter,
                                                                                              genderTransferConverter,
                                                                                              clothesTypeTransferConverter,
                                                                                              colorClothesTransferConverter,
                                                                                              sizeGroupTransferConverter);

            var clothesInformationTransfer = clothesInformationTransferConverter.ToTransfer(clothesInformation);
            var clothesInformationAfterConverter = clothesInformationTransferConverter.FromTransfer(clothesInformationTransfer);

            Assert.True(clothesInformation.Equals(clothesInformationAfterConverter));
        }
    }
}