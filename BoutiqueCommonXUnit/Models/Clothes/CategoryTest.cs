using System;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
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
    }
}