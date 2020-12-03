using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель. Тесты
    /// </summary>
    public class ClothesTypeShortTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ClothesTypeShort_ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.ClothesTypeShortDomains.First();
            var clothesTypeShortTransferConverter = new ClothesTypeShortTransferConverter();

            var clothesTypeShortTransfer = clothesTypeShortTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterShortConverter = clothesTypeShortTransferConverter.FromTransfer(clothesTypeShortTransfer);

            Assert.True(clothesTypeAfterShortConverter.OkStatus);
            Assert.True(clothesType.Equals(clothesTypeAfterShortConverter.Value));
        }
    }
}