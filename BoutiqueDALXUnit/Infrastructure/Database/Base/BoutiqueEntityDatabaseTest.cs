using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Database.Base.EntityDatabaseTable;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Base
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
        }


        ///// <summary>
        ///// Добавить сущности в таблицу. Получить вторую
        ///// </summary>
        //[Fact]
        //public async Task AddRange_Update()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    await testDatabaseTable.AddRangeAsync(entities);
        //    var resultSave = await testDatabase.SaveChangesAsync();

        //    var entityUpdate = entities.Last();
        //    entityUpdate.Name = "entityUpdate";

        //    var resultUpdate = testDatabaseTable.Update(entityUpdate);
        //    var resultAfterUpdate = await testDatabase.SaveChangesAsync();
        //    var entityAfterUpdate = await testDatabaseTable.FindAsync(entityUpdate.Id);

        //    Assert.True(resultSave.OkStatus);
        //    Assert.True(resultUpdate.OkStatus);
        //    Assert.True(resultAfterUpdate.OkStatus);
        //    Assert.Equal(entityUpdate.Name, entityAfterUpdate.Value.Name);
        //}

        ///// <summary>
        ///// Добавить сущности в таблицу. Получить вторую
        ///// </summary>
        //[Fact]
        //public async Task AddRange_Update_NotFound()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    var entityUpdate = entities.Last();
        //    entityUpdate.Name = "entityUpdate";

        //    var resultUpdate = testDatabaseTable.Update(entityUpdate);
        //    var resultAfterUpdate = await testDatabase.SaveChangesAsync();

        //    Assert.True(resultUpdate.OkStatus);
        //    Assert.True(resultAfterUpdate.HasErrors);
        //    Assert.True(resultAfterUpdate.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        //}

        ///// <summary>
        ///// Добавить сущности в таблицу. Удалить первую
        ///// </summary>
        //[Fact]
        //public async Task AddRange_DeleteFirst()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    await testDatabaseTable.AddRangeAsync(entities);
        //    var resultSave = await testDatabase.SaveChangesAsync();

        //    var entityRemove = entities.Last();
        //    var resultRemove = testDatabaseTable.Remove(entityRemove);
        //    var resultAfterRemove = await testDatabase.SaveChangesAsync();
        //    var entityAfterRemove = await testDatabaseTable.ToListAsync();

        //    Assert.True(resultSave.OkStatus);
        //    Assert.True(resultRemove.OkStatus);
        //    Assert.True(resultAfterRemove.OkStatus);
        //    Assert.Equal(entities.Count - 1, entityAfterRemove.Value.Count);
        //}

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
            Options;
    }
}