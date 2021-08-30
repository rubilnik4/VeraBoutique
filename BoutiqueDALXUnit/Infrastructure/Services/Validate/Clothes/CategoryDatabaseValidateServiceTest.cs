using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.CommonErrors;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы категорий одежды
    /// </summary>
    public class CategoryDatabaseValidateServiceTest : CategoryDatabaseValidateService
    {
        public CategoryDatabaseValidateServiceTest()
            : base(CategoryTable.Object,
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
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(result.Errors.First());
        }


        /// <summary>
        /// Проверить модель. Ошибка размеров
        /// </summary>
        [Fact]
        public void ValidateModel_GendersError()
        {
            var category = CategoryData.CategoryMainDomains.First();
            var categoryEmptyGenders = new CategoryMainDomain(category, Enumerable.Empty<IGenderDomain>());

            var result = ValidateModel(categoryEmptyGenders);
            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var category = CategoryData.CategoryMainDomains.First();

            var result = await ValidateIncludes(category);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Типы пола не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_GendersNotFound()
        {
            var genders = GenderData.GenderDomains.Append(new GenderDomain(GenderType.Child, "NotFound"));
            var category = CategoryData.CategoryMainDomains.First();
            var categoryNotFound = new CategoryMainDomain(category, genders);

            var result = await ValidateIncludes(categoryNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static Mock<ICategoryTable> CategoryTable =>
            new();
    }
}