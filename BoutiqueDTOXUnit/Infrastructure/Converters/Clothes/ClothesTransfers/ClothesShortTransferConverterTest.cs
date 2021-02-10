using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesShortTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ClothesShort_ToTransfer_FromTransfer()
        {
            var clothesShort = ClothesData.ClothesShortDomains.First();
            var clothesShortTransferConverter = ClothesTransferConverterMock.ClothesShortTransferConverter;

            var clothesShortTransfer = clothesShortTransferConverter.ToTransfer(clothesShort);
            var clothesShortAfterConverter = clothesShortTransferConverter.FromTransfer(clothesShortTransfer);

            Assert.True(clothesShortAfterConverter.OkStatus);
            Assert.True(clothesShort.Equals(clothesShortAfterConverter.Value));
        }

    }
}