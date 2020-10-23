using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель. Тесты
    /// </summary>
    public class ClothesTypeTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.GetClothesTypeDomain().First();
            var clothesTypeTransferConverter = new ClothesTypeTransferConverter(new CategoryTransferConverter());

            var clothesTypeTransfer = clothesTypeTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesTypeTransfer);

            Assert.True(clothesType.Equals(clothesTypeAfterConverter));
        }
    }
}