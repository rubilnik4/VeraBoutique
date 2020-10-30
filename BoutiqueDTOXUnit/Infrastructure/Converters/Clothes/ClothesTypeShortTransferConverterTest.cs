using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер основных данных вида одежды в трансферную модель. Тесты
    /// </summary>
    public class ClothesTypeShortTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.GetClothesTypeDomain().First();
            var clothesTypeShortTransferConverter = new ClothesTypeShortTransferConverter();

            var clothesTypeShortTransfer = clothesTypeShortTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterShortConverter = clothesTypeShortTransferConverter.FromTransfer(clothesTypeShortTransfer);

            Assert.True(clothesType.Equals(clothesTypeAfterShortConverter));
        }
    }
}