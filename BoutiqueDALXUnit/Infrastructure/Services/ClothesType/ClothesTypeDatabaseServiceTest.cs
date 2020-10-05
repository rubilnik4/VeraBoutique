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

            var clothesTypesResult = await clothesTypeDatabaseService.GetByGenderCategory(gender, category);
            var clothesTypesDomain = clothesTypeEntityConverter.FromEntities(clothesTypes);

            Assert.True(clothesTypesResult.OkStatus);
            Assert.True(clothesTypesResult.Value.SequenceEqual(clothesTypesDomain));
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
            Void(mock => mock.Setup(genderTable => genderTable.Where<(string, GenderType), ClothesTypeGenderEntity>(It.IsAny<GenderType>(), 
                                                                                                                    genderEntity => genderEntity.ClothesTypeGenderEntities)).
                              Returns((GenderType genderType, Expression<Func<GenderEntity, IEnumerable<ClothesTypeGenderEntity>>> include) => genderEntities.Where(genderEntity => genderEntity.Id == genderType).AsQueryable()));

       /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        private static Mock<ICategoryTable> GetCategoryTable(IEnumerable<CategoryEntity> categoryEntities) =>
            new Mock<ICategoryTable>().
            Void(mock => mock.Setup(categoryTable => categoryTable.Where<string, ClothesTypeEntity>(It.IsAny<string>(),
                                                                                                    categoryEntity => categoryEntity.ClothesTypeEntities)).
                              Returns((string category, Expression<Func<CategoryEntity, IEnumerable<ClothesTypeEntity>>> include) => categoryEntities.Where(categoryEntity => categoryEntity.Id == category).AsQueryable()));

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