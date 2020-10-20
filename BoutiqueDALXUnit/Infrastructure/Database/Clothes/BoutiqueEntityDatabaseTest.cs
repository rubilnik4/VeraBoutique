using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable;
using Functional.FunctionalExtensions.Async;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Тесты
    /// </summary>
    public class BoutiqueEntityDatabaseTest
    {
        [Fact]
        public async Task DatabaseTests()
        {
            var genderEntities = GenderInitialize.GenderData;
            var clothesTypeEntities = ClothesTypeInitialize.ClothesTypeData;
            var clothesTypeGenderEntities = ClothesTypeGenderInitialize.ClothesTypeGenderData;
            var categoryEntities = CategoryInitialize.CategoryData;
            using var boutiqueEntityDatabase = await GetTestBoutiqueEntityDatabase();

            await DatabaseLoadTests(boutiqueEntityDatabase, genderEntities, clothesTypeEntities, clothesTypeGenderEntities);
            await DatabaseFindTests(boutiqueEntityDatabase, genderEntities, clothesTypeEntities, clothesTypeGenderEntities);
            await DatabaseWhereTests(boutiqueEntityDatabase, genderEntities, clothesTypeGenderEntities);
            await DatabaseUpdateTests(boutiqueEntityDatabase);
            await BoutiqueEntityDatabaseAdding.AddRange(boutiqueEntityDatabase, categoryEntities);

            using var boutiqueEntityDatabaseDelete = await GetTestBoutiqueEntityDatabase();
            await DatabaseDeleteTests(boutiqueEntityDatabaseDelete, genderEntities, clothesTypeEntities);
        }

        /// <summary>
        /// Тесты добавления и выгрузки элементов в базе
        /// </summary>
        public static async Task DatabaseLoadTests(IBoutiqueDatabase boutiqueEntityDatabase,
                                                   IReadOnlyCollection<GenderEntity> genderEntities,
                                                   IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                   IReadOnlyCollection<ClothesTypeGenderCompositeEntity> clothesTypeGenderEntities)
        {
            await BoutiqueEntityDatabaseGetTest.ToListEntities(boutiqueEntityDatabase, genderEntities,
                                                               clothesTypeEntities, clothesTypeGenderEntities);
            await BoutiqueEntityDatabaseGetTest.AddRange_DuplicateError(boutiqueEntityDatabase, genderEntities);
        }

        /// <summary>
        /// Тесты поиска элементов в базе
        /// </summary>
        public static async Task DatabaseFindTests(IBoutiqueDatabase boutiqueEntityDatabase,
                                                   IReadOnlyCollection<GenderEntity> genderEntities,
                                                   IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                   IReadOnlyCollection<ClothesTypeGenderCompositeEntity> clothesTypeGenderEntities)
        {
            var genderIds = genderEntities.Select(entity => entity.Id).ToList();

            await BoutiqueEntityDatabaseFindTest.FindById(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseFindTest.FindByIds(boutiqueEntityDatabase, genderIds);
            await BoutiqueEntityDatabaseFindTest.FindById_IncludeEntities(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseFindTest.FindByIds_IncludeEntities(boutiqueEntityDatabase, genderIds, clothesTypeGenderEntities);
            await BoutiqueEntityDatabaseFindTest.FindById_NotFound(boutiqueEntityDatabase);
            await BoutiqueEntityDatabaseFindTest.FindByIds_NotFound(boutiqueEntityDatabase, clothesTypeEntities);
        }

        /// <summary>
        /// Тесты поиска элементов в базе
        /// </summary>
        public static async Task DatabaseWhereTests(IBoutiqueDatabase boutiqueEntityDatabase,
                                                    IReadOnlyCollection<GenderEntity> genderEntities,
                                                    IReadOnlyCollection<ClothesTypeGenderCompositeEntity> clothesTypeGenderEntities)
        {
            var genderIds = genderEntities.Select(entity => entity.Id).ToList();

            await BoutiqueEntityDatabaseWhereTest.WhereById(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseWhereTest.WhereByIds(boutiqueEntityDatabase, genderIds);
            await BoutiqueEntityDatabaseWhereTest.WhereById_IncludeEntities(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseWhereTest.WhereByIds_IncludeEntities(boutiqueEntityDatabase, genderIds, clothesTypeGenderEntities);
        }

        /// <summary>
        /// Тесты обновления элементов в базе
        /// </summary>
        public static async Task DatabaseUpdateTests(IBoutiqueDatabase boutiqueEntityDatabase)
        {
            await BoutiqueEntityDatabaseUpdateTest.Update(boutiqueEntityDatabase, GenderType.Male);
            await BoutiqueEntityDatabaseUpdateTest.Update_NotFound(boutiqueEntityDatabase);
        }

        /// <summary>
        /// Тесты удаления элементов в базе
        /// </summary>
        public static async Task DatabaseDeleteTests(IBoutiqueDatabase boutiqueEntityDatabase,
                                                     IReadOnlyCollection<GenderEntity> genderEntities,
                                                     IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities)
        {
            await BoutiqueEntityDatabaseDeleteTest.Delete(boutiqueEntityDatabase, genderEntities);
            await BoutiqueEntityDatabaseDeleteTest.Delete_All(boutiqueEntityDatabase, clothesTypeEntities);
        }

        /// <summary>
        /// База данных в памяти
        /// </summary>
        public static async Task<IBoutiqueDatabase> GetTestBoutiqueEntityDatabase() =>
            await new TestBoutiqueEntityDatabase(BoutiqueEntityDatabaseOptions).
            VoidAsync(database => database.UpdateSchema());

        /// <summary>
        /// Параметры подключения к базе
        /// </summary>
        public static DbContextOptions BoutiqueEntityDatabaseOptions =>
            new DbContextOptionsBuilder().
            UseInMemoryDatabase(Guid.NewGuid().ToString()).
            Options;
    }
}