using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
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
            var clothesTransferConverter = ClothesTransferConverter;

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
            var clothesGenderNull = new ClothesTransfer(clothes, null!, clothes.ClothesTypeShort, clothes.Colors, clothes.SizeGroups);
            var clothesTransferConverter = ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesGenderNull);

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
            var clothesClothesTypeNull = new ClothesTransfer(clothes, clothes.Gender, null!, clothes.Colors, clothes.SizeGroups);
            var clothesTransferConverter = ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesClothesTypeNull);

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
            var clothesColorsNull = new ClothesTransfer(clothes, clothes.Gender, clothes.ClothesTypeShort, null!, clothes.SizeGroups);
            var clothesTransferConverter = ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesColorsNull);

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
            var clothesSIzeGroupsNull = new ClothesTransfer(clothes, clothes.Gender, clothes.ClothesTypeShort, clothes.Colors, null!);
            var clothesTransferConverter = ClothesTransferConverter;

            var clothesAfterConverter = clothesTransferConverter.FromTransfer(clothesSIzeGroupsNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Конвертер информации об одежде в трансферную модель
        /// </summary>
        private static IClothesTransferConverter ClothesTransferConverter =>
              new ClothesTransferConverter(new GenderTransferConverter(),
                                           new ClothesTypeShortTransferConverter(),
                                           new ColorClothesTransferConverter(),
                                           new SizeGroupTransferConverter(new SizeTransferConverter()));
    }
}