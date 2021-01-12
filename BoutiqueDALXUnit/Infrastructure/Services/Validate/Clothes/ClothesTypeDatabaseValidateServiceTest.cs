using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
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
                   CategoryDatabaseValidateServiceMock.GetCategoryDatabaseValidateService(CategoryEntitiesData.CategoryEntities),
                   GenderDatabaseValidateServiceMock.GetGenderDatabaseValidateService(GenderEntitiesData.GenderEntities))
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        [Fact]
        public void ValidateModel_Ok()
        {
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();

            var result = ValidateModel(clothesType);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить модель. Ошибка имени
        /// </summary>
        [Fact]
        public void ValidateModel_NameError()
        {
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeEmptyName = new ClothesTypeDomain(String.Empty, clothesType.Category, clothesType.Genders);

            var result = ValidateModel(clothesTypeEmptyName);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotValid);
        }

        /// <summary>
        /// Проверить модель. Ошибка размеров
        /// </summary>
        [Fact]
        public void ValidateModel_GendersError()
        {
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeEmptyGenders = new ClothesTypeDomain(clothesType, clothesType.Category,
                                                                Enumerable.Empty<IGenderDomain>());

            var result = ValidateModel(clothesTypeEmptyGenders);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.CollectionEmpty);
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_Ok()
        {
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();

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
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeNotFound = new ClothesTypeDomain(clothesType, category, clothesType.Genders);

            var result = await ValidateIncludes(clothesTypeNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Типы пола не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludes_GendersNotFound()
        {
            var genders = GenderData.GendersDomain.Append(new GenderDomain(GenderType.Female, "NotFound"));
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeNotFound = new ClothesTypeDomain(clothesType, clothesType.Category, genders);

            var result = await ValidateIncludes(clothesTypeNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели 
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_Ok()
        {
            var clothesTypes = ClothesTypeData.ClothesTypeDomains.
                               OrderByDescending(clothesType => clothesType.CategoryName);

            var result = await ValidateIncludes(clothesTypes);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить вложенные модели. Типы пола не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_GendersNotFound()
        {
            var genders = GenderData.GendersDomain.Append(new GenderDomain(GenderType.Child, "NotFound"));
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypesNotFound = ClothesTypeData.ClothesTypeDomains.
                                       Append(new ClothesTypeDomain(clothesType, clothesType.Category, genders));

            var result = await ValidateIncludes(clothesTypesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Проверить вложенные модели. Категории не найдены
        /// </summary>
        [Fact]
        public async Task ValidateIncludesCollection_CategoriesNotFound()
        {
            var category = new CategoryDomain("NotFound");
            var clothesType = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypesNotFound = ClothesTypeData.ClothesTypeDomains.
                                       Append(new ClothesTypeDomain(clothesType, category, clothesType.Genders));

            var result = await ValidateIncludes(clothesTypesNotFound);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private static Mock<IClothesTypeTable> ClothesTypeTable =>
            new ();
    }
}