using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
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
        public void ClothesTypeShort_ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.ClothesTypeShortDomain.First();
            var clothesTypeShortTransferConverter = new ClothesTypeShortTransferConverter(new CategoryTransferConverter());

            var clothesTypeShortTransfer = clothesTypeShortTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterShortConverter = clothesTypeShortTransferConverter.FromTransfer(clothesTypeShortTransfer);

            Assert.True(clothesType.Equals(clothesTypeAfterShortConverter));
        }

        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ClothesType_ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.ClothesTypeDomain.First();
            var clothesTypeTransferConverter = new ClothesTypeTransferConverter(new CategoryTransferConverter(),
                                                                                new GenderTransferConverter());

            var clothesTypeTransfer = clothesTypeTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesTypeTransfer);

            Assert.True(clothesType.Equals(clothesTypeAfterConverter));
        }
    }
}