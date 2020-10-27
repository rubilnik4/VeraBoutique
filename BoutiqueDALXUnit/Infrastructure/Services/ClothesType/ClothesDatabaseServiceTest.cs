using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    /// <summary>
    /// Сервис одежды в базе данных. Тесты
    /// </summary>
    public class ClothesDatabaseServiceTest
    {
        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        [Fact]
        public async Task GetClothesShort_Ok()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            var clothesType = clothesTypeEntities.First().Name;
            var clothesInformationEntities = ClothesInformationEntitiesData.ClothesInformationEntities;
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities,
                                                                                            clothesInformationEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesInformationEntities);
            var genderTable = GenderTableMock.GetGenderTable(GenderTableMock.GetGenderOk(genderWithClothesEntities));
            var clothesTypeTable = ClothesTypeTable.GetClothesTypeTable(ClothesTypeTable.GetClothesTypeOk(clothesTypeWithClothesEntities));
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationOk(clothesInformationEntities));
            var clothesShortEntityConverter = ClothesShortEntityConverter;

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    genderTable.Object, clothesTypeTable.Object,
                                                                    clothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter);

            var clothesResults = await clothesDatabaseService.GetClothesShorts(genderType, clothesType);
            var clothesShortDomains = clothesShortEntityConverter.FromEntities(clothesInformationEntities);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesShortDomains.Value));
        }

        /// <summary>
        /// Получить одежду без изображений. Ошибка
        /// </summary>
        [Fact]
        public async Task GetClothesShort_Exception()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            var clothesType = clothesTypeEntities.First().Name;
            var clothesInformationEntities = ClothesInformationEntitiesData.ClothesInformationEntities;
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesInformationEntities);
            var genderTable = GenderTableMock.GetGenderTable(GenderTableMock.GetGenderException());
            var clothesTypeTable = ClothesTypeTable.GetClothesTypeTable(ClothesTypeTable.GetClothesTypeOk(clothesTypeWithClothesEntities));
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationOk(clothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    genderTable.Object, clothesTypeTable.Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter);

            var clothesResults = await clothesDatabaseService.GetClothesShorts(genderType, clothesType);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Ok()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesInformationEntities = ClothesInformationEntitiesData.ClothesInformationEntities;
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationOk(clothesInformationEntities));
            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesInformationDomains.Last().Id);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.Equals(clothesInformationDomains.Last()));
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetIncludesById_NotFound()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationNotFound());

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesInformationDomains.Last().Id);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Exception()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains;
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationException());

            var clothesDatabaseService = new ClothesDatabaseService(Database.Object, clothesTable.Object,
                                                                    new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                                                    ClothesShortEntityConverter,
                                                                    ClothesInformationEntityConverter);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesInformationDomains.Last().Id);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database => new Mock<IDatabase>();



       

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private static IClothesShortEntityConverter ClothesShortEntityConverter =>
            new ClothesShortEntityConverter();

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private static IClothesInformationEntityConverter ClothesInformationEntityConverter =>
            new ClothesInformationEntityConverter(new ClothesShortEntityConverter(),
                                                  new GenderEntityConverter(),
                                                  new ClothesTypeEntityConverter(new CategoryEntityConverter()),
                                                  new ColorClothesEntityConverter(),
                                                  new SizeGroupEntityConverter(new SizeEntityConverter()));
    }
}