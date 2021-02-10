using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных. Тесты
    /// </summary>
    public class CategoryEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var categoryDomain = CategoryData.CategoryDomains.First();
            var categoryEntityConverter = new CategoryEntityConverter();

            var categoryEntity = categoryEntityConverter.ToEntity(categoryDomain);
            var categoryAfterConverter = categoryEntityConverter.FromEntity(categoryEntity);

            Assert.True(categoryAfterConverter.OkStatus);
            Assert.True(categoryDomain.Equals(categoryAfterConverter.Value));
        }
    }
}