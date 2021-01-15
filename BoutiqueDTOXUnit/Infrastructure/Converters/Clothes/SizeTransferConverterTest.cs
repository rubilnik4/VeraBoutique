using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер размеров одежды в трансферную модель. Тесты
    /// </summary>
    public class SizeTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели размеров одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var clothesSize = SizeData.SizeDomain.First();
            var clothesSizeTransferConverter = SizeTransferConverterMock.SizeTransferConverter;

            var clothesSizeTransfer = clothesSizeTransferConverter.ToTransfer(clothesSize);
            var clothesSizeAfterConverter = clothesSizeTransferConverter.FromTransfer(clothesSizeTransfer);

            Assert.True(clothesSizeAfterConverter.OkStatus);
            Assert.True(clothesSize.Equals(clothesSizeAfterConverter.Value));
        }
    }
}