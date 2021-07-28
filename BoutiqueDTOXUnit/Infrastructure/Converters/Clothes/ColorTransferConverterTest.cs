using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер цвета одежды в трансферную модель. Тесты
    /// </summary>
    public class ClothesImageTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var color = ColorData.ColorDomains.First();
            var colorClothesTransferConverter = ColorTransferConverterMock.ColorTransferConverter;

            var colorsTransfer = colorClothesTransferConverter.ToTransfer(color);
            var colorsAfterConverter = colorClothesTransferConverter.FromTransfer(colorsTransfer);

            Assert.True(colorsAfterConverter.OkStatus);
            Assert.True(color.Equals(colorsAfterConverter.Value));
        }
    }
}