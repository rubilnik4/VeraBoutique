using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ClothesTypeTransfers
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
        public void ClothesType_ToTransfer_FromTransfer()
        {
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;

            var clothesTypeTransfer = clothesTypeTransferConverter.ToTransfer(clothesType);
            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesTypeTransfer);

            Assert.True(clothesTypeAfterConverter.OkStatus);
            Assert.True(clothesType.Equals(clothesTypeAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель. Ошибка категории
        /// </summary>
        [Fact]
        public void ClothesType_ToTransfer_GendersCollectionNull()
        {
            var clothesType = ClothesTypeTransfersData.ClothesTypeTransfers.First();
            var clothesTypeNull = new ClothesTypeTransfer(clothesType, clothesType.Category, clothesType.Genders.Append(null));
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;

            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesTypeNull);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}