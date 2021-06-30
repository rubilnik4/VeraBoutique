using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Конвертер уточненной информации об одежде в трансферную модель. Тесты
    /// </summary>
    public class ClothesDetailTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_FromTransfer()
        {
            var clothes = ClothesData.ClothesDetailDomains.First();
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesDetailTransferConverter;

            var clothesTransfer = clothesTransferConverter.ToTransfer(clothes);
            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesTransfer);

            Assert.True(clothesAfterConverter.OkStatus);
            Assert.True(clothes.Equals(clothesAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка цветов
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_ColorsCollectionNull()
        {
            var clothes = ClothesTransfersData.ClothesDetailTransfers.First();
            var clothesNull = new ClothesDetailTransfer(clothes, clothes.Colors.Append(null!)!, clothes.SizeGroups);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesDetailTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка размеров
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_SizeGroupsCollectionNull()
        {
            var clothes = ClothesTransfersData.ClothesMainTransfers.First();
            var clothesNull = new ClothesDetailTransfer(clothes, clothes.Colors, clothes.SizeGroups.Append(null!)!);
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesDetailTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}