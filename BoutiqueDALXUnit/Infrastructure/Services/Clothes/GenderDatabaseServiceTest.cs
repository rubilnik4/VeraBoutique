using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
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
            //var genderEntities = GenderEntitiesData.GenderEntities;
            //var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            //var genderType = genderEntities.First().GenderType;
            //string clothesType = clothesTypeEntities.First().Name;
            //var clothesInformationEntities = ClothesEntitiesData.ClothesEntities;
            //var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities,
            //                                                                                clothesInformationEntities);
            //var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
            //                                                                                               clothesInformationEntities);
            //var genderTable = GenderTableMock.GetGenderTable(genderWithClothesEntities);
            //var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithClothesEntities);
            //var clothesTable = ClothesTableMock.GetClothesTable(clothesInformationEntities);
            var database = GetDatabase(genderTable, clothesTypeTable, clothesTable);
            var genderEntityConverter = GenderEntityConverterMock.GenderEntityConverter;
            var genderCategoryEntityConverter = GenderEntityConverterMock.GenderCategoryEntityConverter;
            var genderDatabaseService = new GenderDatabaseService(database.Object,
                                                                  GetDatabaseValidationService(clothesTable),
                                                                  genderEntityConverter,
                                                                  genderCategoryEntityConverter);

            var clothesResults = await clothesDatabaseService.GetClothes(genderType, clothesType);
            var clothesDomains = clothesEntityConverter.FromEntities(clothesInformationEntities);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesDomains.Value));
        }
    }
}