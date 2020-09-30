using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var genderEntities = EntityData.GenderEntities;
            var clothesType = EntityData.ClothesTypeEntities;

            using var boutiqueEntityDatabase = await GetTestBoutiqueEntityDatabase();

            await BoutiqueEntityDatabaseGetTest.AddRangeEntities(boutiqueEntityDatabase, genderEntities, clothesType);
            await BoutiqueEntityDatabaseGetTest.ToListEntities(boutiqueEntityDatabase, genderEntities, clothesType);
            await DatabaseFindTests(boutiqueEntityDatabase, genderEntities);
            //  await BoutiqueEntityDatabaseUpdateTest.Update(boutiqueEntityDatabase);
            await BoutiqueEntityDatabaseDeleteTest.Delete(boutiqueEntityDatabase, genderEntities.First());
        }

        /// <summary>
        /// Тесты поиска элементов в базе
        /// </summary>
        public static async Task DatabaseFindTests(IBoutiqueDatabase boutiqueEntityDatabase, IList<GenderEntity> genderEntities)
        {
            var genderIds = genderEntities.Select(entity => entity.Id).ToList();

            await BoutiqueEntityDatabaseFindTest.FindById(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseFindTest.FindByIds(boutiqueEntityDatabase, genderIds);
            await BoutiqueEntityDatabaseFindTest.FindById_IncludeEntities(boutiqueEntityDatabase, genderIds.Last());
            await BoutiqueEntityDatabaseFindTest.FindByIds_IncludeEntities(boutiqueEntityDatabase, genderIds);
            await BoutiqueEntityDatabaseFindTest.FindById_NotFound(boutiqueEntityDatabase);
            await BoutiqueEntityDatabaseFindTest.FindByIds_NotFound(boutiqueEntityDatabase, genderIds);
        }

      

       
        ///// <summary>
        ///// Добавить сущности в таблицу. Удалить первую
        ///// </summary>
        //[Fact]
        //public async Task AddRange_DeleteNotFound()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    await testDatabaseTable.AddRangeAsync(entities);
        //    var resultSave = await testDatabase.SaveChangesAsync();

        //    var genderRemove = new TestEntity(TestEnum.Third, "Third");
        //    var resultRemove = testDatabaseTable.Remove(genderRemove);
        //    var resultAfterRemove = await testDatabase.SaveChangesAsync();

        //    Assert.True(resultSave.OkStatus);
        //    Assert.True(resultRemove.OkStatus);
        //    Assert.True(resultAfterRemove.HasErrors);
        //    Assert.True(resultAfterRemove.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        //}

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
            UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).
            Options;
    }
}