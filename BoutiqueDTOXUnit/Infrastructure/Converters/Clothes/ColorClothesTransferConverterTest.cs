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
    public class ColorClothesTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var colors = ColorData.ColorDomains.First();
            var colorClothesTransferConverter = ColorClothesTransferConverterMock.ColorTransferConverter;

            var colorsTransfer = colorClothesTransferConverter.ToTransfer(colors);
            var colorsAfterConverter = colorClothesTransferConverter.FromTransfer(colorsTransfer);

            Assert.True(colorsAfterConverter.OkStatus);
            Assert.True(colors.Equals(colorsAfterConverter.Value));
        }
    }
}