using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTOXUnit.Data.Services.Mocks.Converters;
using BoutiqueDTOXUnit.Data.Transfers;
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
        public void ClothesType_ToTransfer_CategoryNull()
        {
            var clothesType = ClothesTypeTransfersData.ClothesTypeTransfers.First();
            clothesType.Category = null!;
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;

            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesType);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель. Ошибка категории
        /// </summary>
        [Fact]
        public void ClothesType_ToTransfer_GendersNull()
        {
            var clothesType = ClothesTypeTransfersData.ClothesTypeTransfers.First();
            clothesType.Genders = null!;
             var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;

            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesType);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели вида одежды в трансферную модель. Ошибка категории
        /// </summary>
        [Fact]
        public void ClothesType_ToTransfer_GendersCollectionNull()
        {
            var clothesType = ClothesTypeTransfersData.ClothesTypeTransfers.First();
            clothesType.Genders = clothesType.Genders.Append(null).ToList();
            var clothesTypeTransferConverter = ClothesTypeTransferConverterMock.ClothesTypeTransferConverter;

            var clothesTypeAfterConverter = clothesTypeTransferConverter.FromTransfer(clothesType);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}