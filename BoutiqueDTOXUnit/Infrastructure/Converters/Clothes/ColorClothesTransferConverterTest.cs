using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
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
            var colors = ColorClothesData.GetColorClothesDomain().First();
            var colorClothesTransferConverter = new ColorClothesTransferConverter();

            var colorsTransfer = colorClothesTransferConverter.ToTransfer(colors);
            var colorsAfterConverter = colorClothesTransferConverter.FromTransfer(colorsTransfer);

            Assert.True(colors.Equals(colorsAfterConverter));
        }
    }
}