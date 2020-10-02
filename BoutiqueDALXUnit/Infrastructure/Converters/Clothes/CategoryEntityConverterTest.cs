using System.Linq;
using BoutiqueCommonXUnit.Data;
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
            var categoryDomain = CategoryData.GetCategoryDomain().First();
            var categoryEntityConverter = new CategoryEntityConverter();

            var categoryEntity = categoryEntityConverter.ToEntity(categoryDomain);
            var categoryAfterConverter = categoryEntityConverter.FromEntity(categoryEntity);

            Assert.True(categoryDomain.Equals(categoryAfterConverter));
        }
    }
}