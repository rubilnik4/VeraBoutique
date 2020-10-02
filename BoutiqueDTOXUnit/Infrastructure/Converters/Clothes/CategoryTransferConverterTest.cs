﻿using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
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
            var categories =CategoryData.GetCategoryDomain().First();
            var categoryEntityConverter = new CategoryTransferConverter();

            var categoriesTransfer = categoryEntityConverter.ToTransfer(categories);
            var categoriesAfterConverter = categoryEntityConverter.FromTransfer(categoriesTransfer);

            Assert.True(categories.Equals(categoriesAfterConverter));
        }
    }
}