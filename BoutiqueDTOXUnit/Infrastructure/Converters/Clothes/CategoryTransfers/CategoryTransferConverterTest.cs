using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.CategoryTransfers
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель. Тесты
    /// </summary>
    public class CategoryTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели категорий одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var categories = CategoryData.CategoryDomains.First();
            var categoryEntityConverter = CategoryTransferConverterMock.CategoryTransferConverter;

            var categoriesTransfer = categoryEntityConverter.ToTransfer(categories);
            var categoriesAfterConverter = categoryEntityConverter.FromTransfer(categoriesTransfer);

            Assert.True(categoriesAfterConverter.OkStatus);
            Assert.True(categories.Equals(categoriesAfterConverter.Value));
        }
    }
}