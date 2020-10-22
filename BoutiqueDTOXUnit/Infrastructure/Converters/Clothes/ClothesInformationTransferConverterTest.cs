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
            var colorClothesTransferConverter = new ColorClothesTransferConverter();
            var sizeTransferConverter = new SizeTransferConverter();
            var sizeGroupTransferConverter = new SizeGroupTransferConverter(sizeTransferConverter);
            var clothesInformationTransferConverter = new ClothesInformationTransferConverter(colorClothesTransferConverter,
                                                                                              sizeGroupTransferConverter);

            var clothesInformationTransfer = clothesInformationTransferConverter.ToTransfer(clothesInformation);
            var clothesInformationAfterConverter = clothesInformationTransferConverter.FromTransfer(clothesInformationTransfer);

            Assert.True(clothesInformation.Equals(clothesInformationAfterConverter));
        }
    }
}