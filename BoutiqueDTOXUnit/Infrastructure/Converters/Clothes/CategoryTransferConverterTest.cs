using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
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
            var categories =CategoryData.CategoryDomain.First();
            var categoryEntityConverter = CategoryTransferConverterMock.CategoryTransferConverter;

            var categoriesTransfer = categoryEntityConverter.ToTransfer(categories);
            var categoriesAfterConverter = categoryEntityConverter.FromTransfer(categoriesTransfer);

            Assert.True(categoriesAfterConverter.OkStatus);
            Assert.True(categories.Equals(categoriesAfterConverter.Value));
        }
    }
}