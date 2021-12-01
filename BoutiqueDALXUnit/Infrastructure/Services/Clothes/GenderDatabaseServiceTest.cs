using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Clothes
{
    /// <summary>
    /// Сервис типа пола одежды в базе данных. Тесты
    /// </summary>
    public class GenderDatabaseServiceTest
    {
        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        [Fact]
        public async Task GetGenderCategories_Ok()
        {
            var genderCategoryEntities = GenderEntitiesData.GenderCategoryEntities;
            var categoryEntities = CategoryEntitiesData.CategoryEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var categoryWithGenders = CategoryEntitiesData.GetCategoryEntitiesWithGenders(categoryEntities, genderCategoryEntities);
            var clothesTypeWithCategory = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithCategory(clothesTypeEntities, categoryEntities.First());
            var genderTable = GenderTableMock.GetGenderTable(genderCategoryEntities);
            var categoryTable = CategoryTableMock.GetCategoryTable(categoryWithGenders);
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithCategory);
            var database = GetDatabase(genderTable, categoryTable, clothesTypeTable);
            var genderEntityConverter = GenderEntityConverterMock.GenderEntityConverter;
            var genderCategoryEntityConverter = GenderEntityConverterMock.GenderCategoryEntityConverter;
            var genderDatabaseService = new GenderDatabaseService(database.Object,
                                                                  GetDatabaseValidationService(genderTable),
                                                                  genderEntityConverter,
                                                                  genderCategoryEntityConverter);

            var genderResults = await genderDatabaseService.GetGenderCategories();
            var gendersDomains = genderCategoryEntityConverter.FromEntities(genderCategoryEntities);

            Assert.True(genderResults.OkStatus);
            Assert.True(genderResults.Value.SequenceEqual(gendersDomains.Value));
        }

        /// <summary>
        /// Получить одежду без изображений. Ошибка
        /// </summary>
        [Fact]
        public async Task GetGenderCategories_Error()
        {
            var errorInitial = DatabaseErrorData.TableError;
            var genderCategoryEntities = GenderEntitiesData.GenderCategoryEntities;
            var genderResult = new ResultCollection<GenderEntity>(errorInitial);
            var categoryEntities = CategoryEntitiesData.CategoryEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var categoryWithGenders = CategoryEntitiesData.GetCategoryEntitiesWithGenders(categoryEntities, genderCategoryEntities);
            var clothesTypeWithCategory = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithCategory(clothesTypeEntities, categoryEntities.First());
            var genderTable = GenderTableMock.GetGenderTable(genderResult);
            var categoryTable = CategoryTableMock.GetCategoryTable(categoryWithGenders);
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithCategory);
            var database = GetDatabase(genderTable.Object, categoryTable, clothesTypeTable);
            var genderEntityConverter = GenderEntityConverterMock.GenderEntityConverter;
            var genderCategoryEntityConverter = GenderEntityConverterMock.GenderCategoryEntityConverter;
            var genderDatabaseService = new GenderDatabaseService(database.Object,
                                                                  GetDatabaseValidationService(genderTable.Object),
                                                                  genderEntityConverter,
                                                                  genderCategoryEntityConverter);

            var genderResults = await genderDatabaseService.GetGenderCategories();

            Assert.True(genderResults.HasErrors);
            Assert.IsType(errorInitial.GetType(), genderResults.Errors.First());
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IBoutiqueDatabase> GetDatabase(IGenderTable genderTable, ICategoryTable categoryTable,
                                                           IClothesTypeTable clothesTypeTable) =>
            new Mock<IBoutiqueDatabase>().
            Void(mock => mock.Setup(database => database.GendersTable).Returns(genderTable)).
            Void(mock => mock.Setup(database => database.CategoryTable).Returns(categoryTable)).
            Void(mock => mock.Setup(database => database.ClotheTypeTable).Returns(clothesTypeTable));

        /// <summary>
        /// Сервис проверки данных из базы
        /// </summary>
        private static IGenderDatabaseValidateService GetDatabaseValidationService(IGenderTable genderTable) =>
            new GenderDatabaseValidateService(genderTable);
    }
}