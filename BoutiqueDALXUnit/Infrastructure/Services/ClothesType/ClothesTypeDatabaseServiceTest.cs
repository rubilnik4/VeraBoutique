using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks;
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
            var genderTable = GenderTableMock.GetGenderTable(GenderTableMock.GetGenderOk(genderWithClothesTypeEntities));
            var categoryTable = CategoryTableMock.GetCategoryTable(CategoryTableMock.GetCategoryOk(categoryEntitiesWithClothesType));
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;
            var clothesTypeDatabaseService = new ClothesTypeDatabaseService(Database.Object, ClothesTypeTable.Object,
                                                                            genderTable.Object, categoryTable.Object,
                                                                            clothesTypeEntityConverter);

            var clothesTypesResults = await clothesTypeDatabaseService.GetByGenderCategory(gender, category);
            var clothesTypesDomains = clothesTypeEntityConverter.FromEntities(clothesTypeEntities);

            Assert.True(clothesTypesResults.OkStatus);
            Assert.True(clothesTypesResults.Value.SequenceEqual(clothesTypesDomains.Value));
        }

        /// <summary>
        /// Получить вид одежды по типу пола и категории. Ошибка
        /// </summary>
        [Fact]
        public async Task GetByGenderCategory_Exception()
        {
            var genderEntities = GenderEntitiesData.GenderEntities;
            var clothesTypes = ClothesTypeEntitiesData.ClothesTypeEntities;
            var categories = CategoryEntitiesData.CategoryEntities;
            var gender = genderEntities.First().GenderType;
            string category = categories.First().Name;

            var categoryEntitiesWithClothesType = CategoryEntitiesData.GetCategoryEntitiesWithClothesType(categories, clothesTypes);
            var genderTable = GenderTableMock.GetGenderTable(GenderTableMock.GetGenderException());
            var categoryTable = CategoryTableMock.GetCategoryTable(CategoryTableMock.GetCategoryOk(categoryEntitiesWithClothesType));
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;
            var clothesTypeDatabaseService = new ClothesTypeDatabaseService(Database.Object, ClothesTypeTable.Object,
                                                                            genderTable.Object, categoryTable.Object,
                                                                            clothesTypeEntityConverter);

            var clothesTypesResults = await clothesTypeDatabaseService.GetByGenderCategory(gender, category);

            Assert.True(clothesTypesResults.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, clothesTypesResults.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database => 
            new Mock<IDatabase>();

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private static Mock<IClothesTypeTable> ClothesTypeTable => 
            new Mock<IClothesTypeTable>();

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private static IClothesTypeEntityConverter ClothesTypeEntityConverter => 
            new ClothesTypeEntityConverter(new CategoryEntityConverter());
    }
}