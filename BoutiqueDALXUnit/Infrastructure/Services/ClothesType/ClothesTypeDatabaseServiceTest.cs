using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;
namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    /// <summary>
    /// Сервис вида одежды в базе данных. Тесты
    /// </summary>
    public class ClothesTypeDatabaseServiceTest
    {
        /// <summary>
        /// Получить вид одежды по типу пола и категории
        /// </summary>
        [Fact]
        public async Task GetByGenderCategory_Ok()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypeEntities = ClothesTypeEntitiesData.ClothesTypeEntities;
            var categories = CategoryEntitiesData.CategoryEntities;
            var gender = genderEntities.First().GenderType;
            string category = categories.First().Name;

            var genderWithClothesTypeEntities = GenderEntitiesData.GetGenderEntitiesWithClothesType(genderEntities, clothesTypeEntities);
            var categoryEntitiesWithClothesType = CategoryEntitiesData.GetCategoryEntitiesWithClothesType(categories, clothesTypeEntities);
            var genderTable = GenderTableMock.GetGenderTable(genderWithClothesTypeEntities);
            var categoryTable = CategoryTableMock.GetCategoryTable(categoryEntitiesWithClothesType);
            var database = GetDatabase(genderTable, categoryTable);
            var clothesTypeEntityConverter = ClothesTypeEntityConverterMock.ClothesTypeEntityConverter;
            var clothesTypeDatabaseService = new ClothesTypeDatabaseService(database.Object,
                                                                            ClothesTypeDatabaseValidateService,
                                                                            clothesTypeEntityConverter,
                                                                            ClothesTypeEntityConverterMock.ClothesTypeShortEntityConverter);

            var clothesTypesResults = await clothesTypeDatabaseService.GetByGenderCategory(gender, category);
            var clothesTypesDomains = clothesTypeEntityConverter.FromEntities(clothesTypeEntities);

            Assert.True(clothesTypesResults.OkStatus);
            Assert.True(clothesTypesResults.Value.SequenceEqual(clothesTypesDomains.Value));
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IBoutiqueDatabase> GetDatabase(IGenderTable genderTable, ICategoryTable categoryTable) =>
            new Mock<IBoutiqueDatabase>().
            Void(mock => mock.Setup(database => database.GendersTable).Returns(genderTable)).
            Void(mock => mock.Setup(database => database.CategoryTable).Returns(categoryTable));

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private static Mock<IClothesTypeTable> ClothesTypeTable =>
            new Mock<IClothesTypeTable>();

        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        private static Mock<ICategoryDatabaseValidateService> CategoryDatabaseValidateService =>
            new Mock<ICategoryDatabaseValidateService>();

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private static Mock<IGenderDatabaseValidateService> GenderDatabaseValidateService =>
            new Mock<IGenderDatabaseValidateService>();

        /// <summary>
        /// Сервис проверки данных из базы типов одежды
        /// </summary>
        private static IClothesTypeDatabaseValidateService ClothesTypeDatabaseValidateService =>
            new ClothesTypeDatabaseValidateService(ClothesTypeTable.Object,
                                                   CategoryDatabaseValidateService.Object,
                                                   GenderDatabaseValidateService.Object);
    }
}