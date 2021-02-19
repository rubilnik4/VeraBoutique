using System;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using Functional.Models.Enums;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы категорий одежды
    /// </summary>
    public class CategoryDatabaseValidateServiceTest: CategoryDatabaseValidateService
    {
        public CategoryDatabaseValidateServiceTest()
            :base(CategoryTable.Object, 
                  GenderDatabaseValidateServiceMock.GetGenderDatabaseValidateService(GenderEntitiesData.GenderEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var category = CategoryData.CategoryMainDomains.First();

            var result = ValidateModel(category);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var category = CategoryData.CategoryMainDomains.First();
            var categoryEmptyName = new CategoryMainDomain(String.Empty, category.Genders);

            var result = ValidateModel(categoryEmptyName);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static Mock<ICategoryTable> CategoryTable =>
            new();
    }
}