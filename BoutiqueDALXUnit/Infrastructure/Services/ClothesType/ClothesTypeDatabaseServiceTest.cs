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
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using Functional.FunctionalExtensions.Sync;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    using GenderExpression = Expression<Func<GenderEntity, IEnumerable<ClothesTypeGenderEntity>>>;
    using CategoryExpression = Expression<Func<CategoryEntity, IEnumerable<ClothesTypeEntity>>>;

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
            var genderEntities = EntityData.GenderEntities;
            var clothesTypes = EntityData.ClothesTypeEntities;
            var categories = EntityData.CategoryEntities;
            var gender = genderEntities.First().GenderType;
            string category = categories.First().Name;

            var genderWithClothesTypeEntities = EntityData.GetGenderEntitiesWithClothesType(genderEntities, clothesTypes);
            var categoryEntitiesWithClothesType = EntityData.GetCategoryEntitiesWithClothesType(categories, clothesTypes);
            var genderTable = GetGenderTable(genderWithClothesTypeEntities);
            var categoryTable = GetCategoryTable(categoryEntitiesWithClothesType);
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;
            var clothesTypeDatabaseService = new ClothesTypeDatabaseService(Database.Object, ClothesTypeTable.Object,
                                                                            genderTable.Object, categoryTable.Object,
                                                                            clothesTypeEntityConverter, QueryableService.Object);

            var clothesTypesResults = await clothesTypeDatabaseService.GetByGenderCategory(gender, category);
            var clothesTypesDomains = clothesTypeEntityConverter.FromEntities(clothesTypes);

            Assert.True(clothesTypesResults.OkStatus);
            Assert.True(clothesTypesResults.Value.SequenceEqual(clothesTypesDomains));
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database => 
            new Mock<IDatabase>();

        /// <summary>
        /// Таблица базы данных типа пола
        /// </summary>
        private static Mock<IGenderTable> GetGenderTable(IEnumerable<GenderEntity> genderEntities) =>
            new Mock<IGenderTable>().
            Void(mock => mock.Setup(GenderTableWhere).
                              Returns((GenderType genderType, GenderExpression include) => 
                                          genderEntities.Where(genderEntity => genderEntity.Id == genderType).AsQueryable()));

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static Mock<ICategoryTable> GetCategoryTable(IEnumerable<CategoryEntity> categoryEntities) =>
            new Mock<ICategoryTable>().
            Void(mock => mock.Setup(CategoryTableWhere).
                              Returns((string category, CategoryExpression include) =>
                                          categoryEntities.Where(categoryEntity => categoryEntity.Id == category).AsQueryable()));

        /// <summary>
        /// Функция поиска в таблице типа пола
        /// </summary>
        private static Expression<Func<IGenderTable, IQueryable<GenderEntity>>> GenderTableWhere =>
            genderTable => genderTable.Where<(string, GenderType), ClothesTypeGenderEntity>(It.IsAny<GenderType>(),
                                                                                            genderEntity => genderEntity.ClothesTypeGenderEntities);

        /// <summary>
        /// Функция поиска в таблице категории одежды
        /// </summary>
        private static Expression<Func<ICategoryTable, IQueryable<CategoryEntity>>> CategoryTableWhere =>
            categoryTable => categoryTable.Where<string, ClothesTypeEntity>(It.IsAny<string>(),
                                                                            categoryEntity => categoryEntity.ClothesTypeEntities);

        /// <summary>
        /// Сервис обработки запросов базы данных
        /// </summary>
        private static Mock<IQueryableService<string, ClothesTypeEntity>> QueryableService =>
            new Mock<IQueryableService<string, ClothesTypeEntity>>().
            Void(mock => mock.Setup(service => service.ToListAsync(It.IsAny<IEnumerable<ClothesTypeEntity>>())).
                              ReturnsAsync((IEnumerable<ClothesTypeEntity> clothesTypeEntities) => clothesTypeEntities.ToList()));

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        private static Mock<IClothesTypeTable> ClothesTypeTable => 
            new Mock<IClothesTypeTable>();

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private static IClothesTypeEntityConverter ClothesTypeEntityConverter => 
            new ClothesTypeEntityConverter();
    }
}