using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfer;
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
            var clothesType = ClothesTypeData.GetClothesTypeShortDomain().First();
            var clothesTypeShortTransferConverter = new ClothesTypeShortTransferConverter(new CategoryTransferConverter(),
                                                                                          new GenderTransferConverter());

            var clothesTypeShortTransfer = clothesTypeShortTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterShortConverter = clothesTypeShortTransferConverter.FromTransfer(clothesTypeShortTransfer);

            Assert.True(clothesType.Equals(clothesTypeAfterShortConverter));
        }

        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ClothesTypeFull_ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.GetClothesTypeFullDomain().First();
            var clothesTypeTransferConverter = new ClothesTypeFullTransferConverter(new CategoryTransferConverter(),
                                                                                    new GenderTransferConverter());

            var clothesTypeTransfer = clothesTypeTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesTypeTransfer);

            Assert.True(clothesType.Equals(clothesTypeAfterConverter));
        }
    }
}