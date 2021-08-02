using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ImageTransfers
{
    /// <summary>
    /// Конвертер изображений в трансферную модель. Тесты
    /// </summary>
    public class ClothesImageTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели изображений в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var image = ClothesImageData.ClothesImageDomains.First();
            var clothesImageTransferConverter = ClothesImageTransferConverterMock.ClothesImageTransferConverter;

            var imageTransfer = clothesImageTransferConverter.ToTransfer(image);
            var imageAfterConverter = clothesImageTransferConverter.FromTransfer(imageTransfer);

            Assert.True(imageAfterConverter.OkStatus);
            Assert.True(image.Equals(imageAfterConverter.Value));
        }
    }
}