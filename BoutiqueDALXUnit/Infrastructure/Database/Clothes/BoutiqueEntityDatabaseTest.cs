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
            using var boutiqueEntityDatabase = await GetTestBoutiqueEntityDatabase();

            await DatabaseLoadTests(boutiqueEntityDatabase, genderEntities, clothesTypeEntities, clothesTypeGenderEntities);
            await DatabaseFindTests(boutiqueEntityDatabase, genderEntities, clothesTypeGenderEntities);
            await DatabaseUpdateTests(boutiqueEntityDatabase);

            using var boutiqueEntityDatabaseDelete = await GetTestBoutiqueEntityDatabase();
            await DatabaseDeleteTests(boutiqueEntityDatabaseDelete, genderEntities, clothesTypeEntities, clothesTypeGenderEntities);
            await DatabaseAddingTests(boutiqueEntityDatabase);
        }

        /// <summary>
        /// Тесты добавления и выгрузки элементов в базе
        /// </summary>
        public static async Task DatabaseLoadTests(IBoutiqueDatabase boutiqueEntityDatabase,
                                                   IReadOnlyCollection<GenderEntity> genderEntities,
                                                   IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                   IReadOnlyCollection<ClothesTypeGenderEntity> clothesTypeGenderEntities)
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
                                                   IReadOnlyCollection<ClothesTypeGenderEntity> clothesTypeGenderEntities)
        {
            var genderIds = genderEntities.Select(entity => entity.Id).ToList();

            await BoutiqueEntityDatabaseFindTest.FindById(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseFindTest.FindByIds(boutiqueEntityDatabase, genderIds);
            await BoutiqueEntityDatabaseFindTest.FindById_IncludeEntities(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseFindTest.FindByIds_IncludeEntities(boutiqueEntityDatabase, genderIds, clothesTypeGenderEntities);
            await BoutiqueEntityDatabaseFindTest.FindById_NotFound(boutiqueEntityDatabase);
            await BoutiqueEntityDatabaseFindTest.FindByIds_NotFound(boutiqueEntityDatabase, genderIds);
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
                                                     IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                     IReadOnlyCollection<ClothesTypeGenderEntity> clothesTypeGenderEntities)
        {
            await BoutiqueEntityDatabaseDeleteTest.Delete(boutiqueEntityDatabase, genderEntities);
            await BoutiqueEntityDatabaseDeleteTest.Delete_All(boutiqueEntityDatabase, genderEntities, clothesTypeEntities, 
                                                              clothesTypeGenderEntities);
        }

        /// <summary>
        /// Тесты добавления элементов в базе
        /// </summary>
        public static async Task DatabaseAddingTests(IBoutiqueDatabase boutiqueEntityDatabase)
        {
            await BoutiqueEntityDatabaseGetTest.AddRangeEntities(boutiqueEntityDatabase);
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