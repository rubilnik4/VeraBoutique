using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
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
            var clothesInformationEntities = ClothesEntitiesData.ClothesEntities;
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities,
                                                                                            clothesInformationEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesInformationEntities);
            var genderTable = GenderTableMock.GetGenderTable(GenderTableMock.GetGenderOk(genderWithClothesEntities));
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(ClothesTypeTableMock.GetClothesTypeOk(clothesTypeWithClothesEntities));
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationOk(clothesInformationEntities));
            var clothesShortEntityConverter = ClothesEntityConverterMock.ClothesShortEntityConverter;
            var database = GetDatabase(genderTable.Object, clothesTypeTable.Object, clothesTable.Object);
            var clothesDatabaseService = new ClothesDatabaseService(database.Object,
                                                                    GetDatabaseValidationService(clothesTable.Object), 
                                                                    clothesShortEntityConverter, 
                                                                    ClothesEntityConverterMock.ClothesEntityConverter);

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
            var clothesInformationEntities = ClothesEntitiesData.ClothesEntities;
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesInformationEntities);
            var genderTable = GenderTableMock.GetGenderTable(GenderTableMock.GetGenderException());
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(ClothesTypeTableMock.GetClothesTypeOk(clothesTypeWithClothesEntities));
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationOk(clothesInformationEntities));
            var database = GetDatabase(genderTable.Object, clothesTypeTable.Object, clothesTable.Object);
            var clothesDatabaseService = new ClothesDatabaseService(database.Object,
                                                                    GetDatabaseValidationService(clothesTable.Object),
                                                                    ClothesEntityConverterMock.ClothesShortEntityConverter,
                                                                    ClothesEntityConverterMock.ClothesEntityConverter);

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
            var clothesDomains = ClothesData.ClothesDomains;
            var clothesEntities = ClothesEntitiesData.ClothesEntities;
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationOk(clothesEntities));
            var database = GetDatabase(new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                       clothesTable.Object);
            var clothesDatabaseService = new ClothesDatabaseService(database.Object,
                                                                    GetDatabaseValidationService(clothesTable.Object),
                                                                    ClothesEntityConverterMock.ClothesShortEntityConverter,
                                                                    ClothesEntityConverterMock.ClothesEntityConverter);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesDomains.Last().Id);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.Equals(clothesDomains.Last()));
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetIncludesById_NotFound()
        {
            var clothesDomains = ClothesData.ClothesDomains;
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationNotFound());
            var database = GetDatabase(new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                       clothesTable.Object);

            var clothesDatabaseService = new ClothesDatabaseService(database.Object,
                                                                    GetDatabaseValidationService(clothesTable.Object),
                                                                    ClothesEntityConverterMock.ClothesShortEntityConverter,
                                                                    ClothesEntityConverterMock.ClothesEntityConverter);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesDomains.Last().Id);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить информацию об одежде по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetIncludesById_Exception()
        {
            var clothesDomains = ClothesData.ClothesDomains;
            var clothesTable = ClothesTableMock.GetClothesTable(ClothesTableMock.GetClothesInformationException());
            var database = GetDatabase(new Mock<IGenderTable>().Object, new Mock<IClothesTypeTable>().Object,
                                       clothesTable.Object);
            var clothesDatabaseService = new ClothesDatabaseService(database.Object,
                                                                    GetDatabaseValidationService(clothesTable.Object),
                                                                    ClothesEntityConverterMock.ClothesShortEntityConverter,
                                                                    ClothesEntityConverterMock.ClothesEntityConverter);

            var clothesResults = await clothesDatabaseService.GetIncludesById(clothesDomains.Last().Id);

            Assert.True(clothesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, clothesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IBoutiqueDatabase> GetDatabase(IGenderTable genderTable, IClothesTypeTable clothesTypeTable,
                                                           IClothesTable clothesTable) => 
            new Mock<IBoutiqueDatabase>().
            Void(mock => mock.Setup(database => database.GendersTable).Returns(genderTable)).
            Void(mock => mock.Setup(database => database.ClotheTypeTable).Returns(clothesTypeTable)).
            Void(mock => mock.Setup(database => database.ClothesTable).Returns(clothesTable));

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IGenderDatabaseValidateService> GenderDatabaseValidateService =>
            new Mock<IGenderDatabaseValidateService>();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IClothesTypeDatabaseValidateService> ClothesTypeDatabaseValidateService =>
            new Mock<IClothesTypeDatabaseValidateService>();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IColorClothesDatabaseValidateService> ColorClothesDatabaseValidateService =>
            new Mock<IColorClothesDatabaseValidateService>();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<ISizeGroupDatabaseValidateService> SizeGroupDatabaseValidateService =>
            new Mock<ISizeGroupDatabaseValidateService>();

        /// <summary>
        /// Сервис проверки данных из базы
        /// </summary>
        private static IClothesDatabaseValidateService GetDatabaseValidationService(IClothesTable clothesTable) =>
            new ClothesDatabaseValidateService(clothesTable,
                                               GenderDatabaseValidateService.Object,
                                               ClothesTypeDatabaseValidateService.Object,
                                               ColorClothesDatabaseValidateService.Object,
                                               SizeGroupDatabaseValidateService.Object);
    }
}