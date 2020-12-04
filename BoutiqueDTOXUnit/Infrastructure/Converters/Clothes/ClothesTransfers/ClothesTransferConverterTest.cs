using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Data.Services.Mocks.Converters;
using BoutiqueDTOXUnit.Data.Transfers;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель. Тесты
    /// </summary>
    public class ClothesTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_FromTransfer()
        {
            var clothes = ClothesData.ClothesDomains.First();
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesTransfer = clothesTransferConverter.ToTransfer(clothes);
            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesTransfer);

            Assert.True(clothesAfterConverter.OkStatus);
            Assert.True(clothes.Equals(clothesAfterConverter.Value));
        }


        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка пола
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_GenderNull()
        {
            var clothes = ClothesTransfersData.ClothesTransfers.First();
            clothes.Gender = null!;
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothes);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка типа одежды
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_ClothesTypeNull()
        {
            var clothes = ClothesTransfersData.ClothesTransfers.First();
            clothes.ClothesTypeShort = null!;
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothes);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка цветов
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_ColorsNull()
        {
            var clothes = ClothesTransfersData.ClothesTransfers.First();
            clothes.Colors = null!;
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothes);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка цветов
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_ColorsCollectionNull()
        {
            var clothes = ClothesTransfersData.ClothesTransfers.First();
            clothes.Colors = clothes.Colors.Append(null).ToList();
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothes);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка размеров
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_SizeGroupsNull()
        {
            var clothes = ClothesTransfersData.ClothesTransfers.First();
            clothes.SizeGroups = null!;
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothes);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в трансферную модель. Ошибка размеров
        /// </summary>
        [Fact]
        public void Clothes_ToTransfer_SizeGroupsCollectionNull()
        {
            var clothes = ClothesTransfersData.ClothesTransfers.First();
            clothes.SizeGroups = clothes.SizeGroups.Append(null).ToList();
            var clothesTransferConverter = ClothesTransferConverterMock.ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothes);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}