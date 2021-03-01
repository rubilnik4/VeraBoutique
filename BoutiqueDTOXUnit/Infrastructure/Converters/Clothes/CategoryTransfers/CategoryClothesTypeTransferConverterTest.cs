using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.CategoryTransfers
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель. Тесты
    /// </summary>
    public class CategoryClothesTypeTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели категорий одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var categories = CategoryData.CategoryClothesTypeDomains.First();
            var categoryClothesTypeTransferConverter = CategoryTransferConverterMock.CategoryClothesTypeTransferConverter;

            var categoriesTransfer = categoryClothesTypeTransferConverter.ToTransfer(categories);
            var categoriesAfterConverter = categoryClothesTypeTransferConverter.FromTransfer(categoriesTransfer);

            Assert.True(categoriesAfterConverter.OkStatus);
            Assert.True(categories.Equals(categoriesAfterConverter.Value));
        }
    }
}