using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер одежды в трансферную модель. Тесты
    /// </summary>
    public class ClothesShortTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var clothesShort = ClothesData.ClothesShortDomains.First();
            var clothesShortTransferConverter = new ClothesShortTransferConverter();

            var clothesShortTransfer = clothesShortTransferConverter.ToTransfer(clothesShort);
            var clothesShortAfterConverter = clothesShortTransferConverter.FromTransfer(clothesShortTransfer);

            Assert.True(clothesShort.Equals(clothesShortAfterConverter));
        }
    }
}