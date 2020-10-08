using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType
{
    /// <summary>
    /// Сервис группы размеров одежды в базе данных. Тесты
    /// </summary>
    public class SizeGroupDatabaseServiceTest
    {
        /// <summary>
        /// Получить вид одежды по типу пола и категории
        /// </summary>
        [Fact]
        public async Task GetSizeGroupsIncludeSize_Ok()
        {
            var sizeGroupInitial = SizeGroupData.GetSizeGroupDomain().First();
            var clothesSizeType = sizeGroupInitial.ClothesSizeType;
            int sizeNormalize = sizeGroupInitial.SizeNormalize;
            //var genderEntities = EntityData.GenderEntities;
            var sizeGroups = EntityData.();
            //var categories = EntityData.CategoryEntities;
            //var gender = genderEntities.First().GenderType;
            //string category = categories.First().Name;

            //var genderWithClothesTypeEntities = EntityData.GetGenderEntitiesWithClothesType(genderEntities, clothesTypes);
            //var categoryEntitiesWithClothesType = EntityData.GetCategoryEntitiesWithClothesType(categories, clothesTypes);
            //var genderTable = GetGenderTable(genderWithClothesTypeEntities);
            //var categoryTable = GetCategoryTable(categoryEntitiesWithClothesType);
            var sizeGroupEntityConverter = SizeGroupEntityConverter;
            var sizeDatabaseService = new SizeGroupDatabaseService(Database.Object, ClothesTypeTable.Object,
                                                                   sizeGroupEntityConverter);

            var sizeGroupResults = await sizeDatabaseService.GetSizeGroupsIncludeSize(clothesSizeType, sizeNormalize);
            var sizeGroupDomains = sizeGroupEntityConverter.FromEntities(sizeGroups);

            Assert.True(sizeGroupResults.OkStatus);
            Assert.True(sizeGroupResults.Value.SequenceEqual(sizeGroupDomains));
        }

        /// <summary>
        /// База данных
        /// </summary>
        private static Mock<IDatabase> Database =>
            new Mock<IDatabase>();

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        private static Mock<ISizeGroupTable> GetSizeGroupTable(IEnumerable<SizeGroupEntity> sizeGroupEntities) =>
            new Mock<ISizeGroupTable>().
            Void(mock => mock.Setup(SizeGroupTableToList).
                              Returns((GenderType genderType, genderExpression include) =>
                                          genderEntities.Where(genderEntity => genderEntity.Id == genderType).AsQueryable()));

        /// <summary>
        /// Функция выгрузки в таблице группы размеров одежды
        /// </summary>
        private static Expression<Func<ISizeGroupTable, IResultCollection<SizeGroupEntity>>> SizeGroupTableToList =>
            sizeGroupTable => sizeGroupTable.ToListAsync<(SizeType, string, ClothesSizeType, int), SizeGroupCompositeEntity>(
                sizeGroupEntity => sizeGroupEntity.SizeGroupCompositeEntities);

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        private static ISizeGroupEntityConverter SizeGroupEntityConverter =>
            new SizeGroupEntityConverter(new SizeEntityConverter());

    }
}