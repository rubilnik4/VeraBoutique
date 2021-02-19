using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели одежды в трансферную модель
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_FromTransfer()
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesTransfer = clothesTransferConverter.ToTransfer(clothes);
            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesTransfer);

            Assert.True(clothesAfterConverter.OkStatus);
            Assert.True(clothesTransfer.Equals(clothesAfterConverter.Value));
        }

    }
}