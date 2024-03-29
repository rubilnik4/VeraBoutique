﻿using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Entities.Clothes;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Results;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Clothes
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
        public async Task GetClothes_Ok()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            string clothesType = clothesTypeEntities.First().Name;
            var clothesEntities = ClothesEntitiesData.ClothesEntities;
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities, clothesEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesEntities);
            var genderTable = GenderTableMock.GetGenderTable(genderWithClothesEntities);
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithClothesEntities);
            var clothesTable = ClothesTableMock.GetClothesTable(clothesEntities);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesEntityConverter;
            var database = GetDatabase(genderTable, clothesTypeTable, clothesTable);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable));

            var clothesResults = await clothesDatabaseService.GetClothes(genderType, clothesType);
            var clothesDomains = clothesEntityConverter.FromEntities(clothesEntities);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesDomains.Value.Where(clothes => clothes.GenderType == genderType &&
                                                                                                clothes.ClothesTypeName == clothesType)));
        }

        /// <summary>
        /// Получить одежду без изображений. Ошибка
        /// </summary>
        [Fact]
        public async Task GetClothes_Error()
        {
            var errorInitial = DatabaseErrorData.TableError;
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            string clothesType = clothesTypeEntities.First().Name;
            var clothesEntities = ClothesEntitiesData.ClothesEntities;
            var clothesResult = new ResultCollection<ClothesEntity>(errorInitial);
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities, clothesEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesEntities);
            var genderTable = GenderTableMock.GetGenderTable(genderWithClothesEntities);
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithClothesEntities);
            var clothesTable = ClothesTableMock.GetClothesTable(clothesResult);
            var database = GetDatabase(genderTable, clothesTypeTable, clothesTable.Object);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable.Object));

            var clothesResults = await clothesDatabaseService.GetClothes(genderType, clothesType);

            Assert.True(clothesResults.HasErrors);
            Assert.IsType(errorInitial.GetType(), clothesResults.Errors.First());
        }

        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        [Fact]
        public async Task GetClothesDetails_Ok()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            string clothesType = clothesTypeEntities.First().Name;
            var clothesEntities = ClothesEntitiesData.ClothesEntities;
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities, clothesEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesEntities);
            var genderTable = GenderTableMock.GetGenderTable(genderWithClothesEntities);
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithClothesEntities);
            var clothesTable = ClothesTableMock.GetClothesTable(clothesEntities);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesDetailEntityConverter;
            var database = GetDatabase(genderTable, clothesTypeTable, clothesTable);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable));

            var clothesResults = await clothesDatabaseService.GetClothesDetails(genderType, clothesType);
            var clothesDomains = clothesEntityConverter.FromEntities(clothesEntities);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(clothesDomains.Value.Where(clothes => clothes.GenderType == genderType &&
                                                                                                 clothes.ClothesTypeName == clothesType)));
        }

        /// <summary>
        /// Получить одежду без изображений. Ошибка
        /// </summary>
        [Fact]
        public async Task GetClothesDetails_Error()
        {
            var errorInitial = DatabaseErrorData.TableError;
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var genderType = genderEntities.First().GenderType;
            string clothesType = clothesTypeEntities.First().Name;
            var clothesEntities = ClothesEntitiesData.ClothesEntities;
            var clothesResult = new ResultCollection<ClothesEntity>(errorInitial);
            var genderWithClothesEntities = GenderEntitiesData.GetGenderEntitiesWithClothes(genderEntities, clothesEntities);
            var clothesTypeWithClothesEntities = ClothesTypeEntitiesData.GetClothesTypeEntitiesWithClothes(clothesTypeEntities,
                                                                                                           clothesEntities);
            var genderTable = GenderTableMock.GetGenderTable(genderWithClothesEntities);
            var clothesTypeTable = ClothesTypeTableMock.GetClothesTypeTable(clothesTypeWithClothesEntities);
            var clothesTable = ClothesTableMock.GetClothesTable(clothesResult);
            var database = GetDatabase(genderTable, clothesTypeTable, clothesTable.Object);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable.Object));

            var clothesResults = await clothesDatabaseService.GetClothesDetails(genderType, clothesType);

            Assert.True(clothesResults.HasErrors);
            Assert.IsType(errorInitial.GetType(), clothesResults.Errors.First());
        }

        /// <summary>
        /// Получить изображение
        /// </summary>
        [Fact]
        public async Task GetImage_Ok()
        {
            var clothesEntities = ClothesEntitiesData.ClothesEntities;
            var clothesEntity = clothesEntities.First();
            var image = clothesEntity.ClothesImages!.First(imageEntity => imageEntity.IsMain).Image;
            var clothesTable = ClothesTableMock.GetClothesTable(clothesEntities);
            var database = GetDatabase(clothesTable);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable));

            var clothesResults = await clothesDatabaseService.GetImage(clothesEntity.Id);

            Assert.True(clothesResults.OkStatus);
            Assert.True(clothesResults.Value.SequenceEqual(image));
        }

        /// <summary>
        /// Получить изображение. Ошибка базы
        /// </summary>
        [Fact]
        public async Task GetImage_ErrorDatabase()
        {
            var clothesEntity = ClothesEntitiesData.ClothesEntities.First();
            var errorInitial = DatabaseErrorData.TableError;
            var clothesResult = new ResultValue<ClothesImageEntity>(errorInitial);
            var clothesTable = ClothesTableMock.GetClothesTable(clothesResult);
            var database = GetDatabase(clothesTable.Object);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable.Object));

            var clothesResults = await clothesDatabaseService.GetImage(clothesEntity.Id);

            Assert.True(clothesResults.HasErrors);
            Assert.IsType(errorInitial.GetType(), clothesResults.Errors.First());
        }

        /// <summary>
        /// Получить изображение. Отсутствие значения
        /// </summary>
        [Fact]
        public async Task GetImage_NotFound()
        {
            var clothesEntity = ClothesEntitiesData.ClothesEntities.First();
            var errorInitial = DatabaseErrorData.NotFoundError;
            var clothesResult = new ResultValue<ClothesImageEntity>(errorInitial);
            var clothesTable = ClothesTableMock.GetClothesTable(clothesResult);
            var database = GetDatabase(clothesTable.Object);
            var clothesDatabaseService = GetClothesDatabaseService(database.Object, GetDatabaseValidationService(clothesTable.Object));

            var clothesResults = await clothesDatabaseService.GetImage(clothesEntity.Id);

            Assert.True(clothesResults.HasErrors);
            Assert.IsType(errorInitial.GetType(), clothesResults.Errors.First());
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
        /// База данных
        /// </summary>
        private static Mock<IBoutiqueDatabase> GetDatabase(IClothesTable clothesTable) =>
            new Mock<IBoutiqueDatabase>().
            Void(mock => mock.Setup(database => database.ClothesTable).Returns(clothesTable));

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IGenderDatabaseValidateService> GenderDatabaseValidateService =>
            new ();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IClothesTypeDatabaseValidateService> ClothesTypeDatabaseValidateService =>
            new();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IColorClothesDatabaseValidateService> ColorClothesDatabaseValidateService =>
            new();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<ISizeGroupDatabaseValidateService> SizeGroupDatabaseValidateService =>
            new();


        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IClothesImageDatabaseValidateService> ClothesImageDatabaseValidateService =>
            new();

        /// <summary>
        /// Сервис проверки данных из базы
        /// </summary>
        private static IClothesDatabaseValidateService GetDatabaseValidationService(IClothesTable clothesTable) =>
            new ClothesDatabaseValidateService(clothesTable,
                                               GenderDatabaseValidateService.Object,
                                               ClothesTypeDatabaseValidateService.Object,
                                               ColorClothesDatabaseValidateService.Object,
                                               SizeGroupDatabaseValidateService.Object,
                                               ClothesImageDatabaseValidateService.Object);

        /// <summary>
        /// Получить сервис базы данных с одеждой
        /// </summary>
        private static IClothesDatabaseService GetClothesDatabaseService(IBoutiqueDatabase database,
                                                                         IClothesDatabaseValidateService clothesDatabaseValidateService) =>
            new ClothesDatabaseService(database, clothesDatabaseValidateService,
                                       ClothesEntityConverterMock.ClothesEntityConverter,
                                       ClothesEntityConverterMock.ClothesDetailEntityConverter,
                                       ClothesEntityConverterMock.ClothesMainEntityConverter,
                                       ClothesImageEntityConverterMock.ClothesImageEntityConverter);
    }
}