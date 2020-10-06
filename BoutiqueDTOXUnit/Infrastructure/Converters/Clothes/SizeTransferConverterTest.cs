using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
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
            var clothesSize = ClothesSizeData.GetClothesSizeDomain().First();
            var clothesSizeTransferConverter = new SizeTransferConverter();

            var clothesSizeTransfer = clothesSizeTransferConverter.ToTransfer(clothesSize);
            var clothesSizeAfterConverter = clothesSizeTransferConverter.FromTransfer(clothesSizeTransfer);

            Assert.True(clothesSize.Equals(clothesSizeAfterConverter));
        }
    }
}