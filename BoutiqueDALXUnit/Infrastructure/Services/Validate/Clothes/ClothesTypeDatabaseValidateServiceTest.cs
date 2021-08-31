using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors.DatabaseErrors;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;
using ClothesTypeDomain = BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains.ClothesTypeDomain;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Clothes
{
    /// <summary>
    /// Сервис проверки данных из базы типов одежды
    /// </summary>
    public class ClothesTypeDatabaseValidateServiceTest : ClothesTypeDatabaseValidateService
    {
        public ClothesTypeDatabaseValidateServiceTest()
            : base(ClothesTypeTable.Object,
                   CategoryDatabaseValidateServiceMock.GetCategoryDatabaseValidateService(CategoryEntitiesData.CategoryEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var clothesType = ClothesTypeData.ClothesTypeMainDomains.First();

            var result = ValidateModel(clothesType);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель. Ошибка имени
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var clothesType = ClothesTypeData.ClothesTypeMainDomains.First();
            var clothesTypeEmptyName = new ClothesTypeMainDomain(String.Empty, SizeType.American, clothesType.Category);

            var result = ValidateModel(clothesTypeEmptyName);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var clothesType = ClothesTypeData.ClothesTypeMainDomains.First();

            var result = await ValidateIncludes(clothesType);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Категория не найдена
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_CategoryNotFound()
        {
            var category = new CategoryDomain("NotFound");
            var clothesType = ClothesTypeData.ClothesTypeMainDomains.First();
            var clothesTypeNotFound = new ClothesTypeMainDomain(clothesType, category);

            var result = await ValidateIncludes(clothesTypeNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_Ok()
        {
            var clothesTypes = ClothesTypeData.ClothesTypeMainDomains.
                               OrderByDescending(clothesType => clothesType.CategoryName);

            var result = await ValidateIncludes(clothesTypes);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Категории не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_CategoriesNotFound()
        {
            var category = new CategoryDomain("NotFound");
            var clothesType = ClothesTypeData.ClothesTypeMainDomains.First();
            var clothesTypesNotFound = ClothesTypeData.ClothesTypeMainDomains.
                                       Append(new ClothesTypeMainDomain(clothesType, category));

            var result = await ValidateIncludes(clothesTypesNotFound);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private static Mock<IClothesTypeTable> ClothesTypeTable =>
            new ();
    }
}