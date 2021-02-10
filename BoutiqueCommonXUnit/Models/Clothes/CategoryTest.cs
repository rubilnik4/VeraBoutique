using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Категория одежды. Тесты
    /// </summary>
    public class CategoryTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Category_Equal_Ok()
        {
            const string category = "обувь";

            var categoryDomain = new CategoryDomain(category);

            int categoryHash = HashCode.Combine(category);
            Assert.Equal(categoryHash, categoryDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void Color_Equal_Color()
        {
            var first = CategoryData.CategoryDomains.First();
            var second = CategoryData.CategoryDomains.First();

            Assert.True(first.Equals(second));
        }
    }
}