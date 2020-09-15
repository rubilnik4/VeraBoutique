using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Base
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Тесты
    /// </summary>
    public class EntityDatabaseTableTest
    {
        /// <summary>
        /// Добавить сущности в таблицу. Проверить идентификаторы
        /// </summary>
        [Fact]
        public async Task AddRange_CheckEntities()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            var ids = await testDatabaseTable.AddRangeAsync(entities);
            var result = await testDatabase.SaveChangesAsync();

            Assert.True(result.OkStatus);
            Assert.True(ids.OkStatus);
            Assert.True(ids.Value.SequenceEqual(entities.Select(entity => entity.TestEnum)));
        }

        /// <summary>
        /// Добавить одинаковые данные дважды. Ошибка
        /// </summary>
        [Fact]
        public async Task AddRange_DuplicateError()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            await testDatabaseTable.AddRangeAsync(entities);
            var firstResult = await testDatabase.SaveChangesAsync();

            await testDatabaseTable.AddRangeAsync(entities);
            var secondResult = await testDatabase.SaveChangesAsync();

            Assert.True(firstResult.OkStatus);
            Assert.True(secondResult.HasErrors);
            Assert.True(secondResult.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить сущности из таблицы
        /// </summary>
        [Fact]
        public async Task AddRange_GetEntities()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            await testDatabaseTable.AddRangeAsync(entities);
            var result = await testDatabase.SaveChangesAsync();
            var gendersGet = await testDatabaseTable.ToListAsync();

            Assert.True(result.OkStatus);
            Assert.True(gendersGet.OkStatus);
            Assert.True(entities.SequenceEqual(gendersGet.Value));
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить вторую
        /// </summary>
        [Fact]
        public async Task AddRange_GetSecond()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            var ids = await testDatabaseTable.AddRangeAsync(entities);
            var result = await testDatabase.SaveChangesAsync();
            var getId = ids.Value.Last();
            var entityGet = await testDatabaseTable.FirstAsync(getId);

            Assert.True(result.OkStatus);
            Assert.True(entityGet.OkStatus);
            Assert.True(entityGet.Value.Equals(entities.First(entity => entity.TestEnum == getId)));
        }

        /// <summary>
        /// Добавить сущности в таблицу. Элемент не найден
        /// </summary>
        [Fact]
        public async Task AddRange_GetNotFound()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            await testDatabaseTable.AddRangeAsync(entities);
            var result = await testDatabase.SaveChangesAsync();
            var genderGet = await testDatabaseTable.FirstAsync(TestEnum.Third);

            Assert.True(result.OkStatus);
            Assert.True(genderGet.HasErrors);
            Assert.True(genderGet.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Найти их по идентификатору
        /// </summary>
        [Fact]
        public async Task AddRange_FindAll()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            var ids = await testDatabaseTable.AddRangeAsync(entities);
            var result = await testDatabase.SaveChangesAsync();
            var testFind = await testDatabaseTable.FindAsync(ids.Value);

            Assert.True(result.OkStatus);
            Assert.True(testFind.OkStatus);
            Assert.True(testFind.Value.SequenceEqual(entities));
        }

        /// <summary>
        /// Добавить сущности в таблицу. Отсутствие в базе
        /// </summary>
        [Fact]
        public async Task AddRange_Find_NotFound()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = TestData.GetTestDomains();
            var ids = TestData.GetTestIds(entities);

            var testFind = await testDatabaseTable.FindAsync(ids);

            Assert.True(testFind.OkStatus);
            Assert.True(testFind.Value.Count == 0);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить вторую
        /// </summary>
        [Fact]
        public async Task AddRange_Update()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            await testDatabaseTable.AddRangeAsync(entities);
            var resultSave = await testDatabase.SaveChangesAsync();

            var entityUpdate = entities.Last();
            entityUpdate.Name = "entityUpdate";

            var resultUpdate = testDatabaseTable.Update(entityUpdate);
            var resultAfterUpdate = await testDatabase.SaveChangesAsync();
            var entityAfterUpdate = await testDatabaseTable.FirstAsync(entityUpdate.Id);

            Assert.True(resultSave.OkStatus);
            Assert.True(resultUpdate.OkStatus);
            Assert.True(resultAfterUpdate.OkStatus);
            Assert.Equal(entityUpdate.Name, entityAfterUpdate.Value.Name);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Получить вторую
        /// </summary>
        [Fact]
        public async Task AddRange_Update_NotFound()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            var entityUpdate = entities.Last();
            entityUpdate.Name = "entityUpdate";

            var resultUpdate = testDatabaseTable.Update(entityUpdate);
            var resultAfterUpdate = await testDatabase.SaveChangesAsync();

            Assert.True(resultUpdate.OkStatus);
            Assert.True(resultAfterUpdate.HasErrors);
            Assert.True(resultAfterUpdate.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Удалить первую
        /// </summary>
        [Fact]
        public async Task AddRange_DeleteFirst()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            await testDatabaseTable.AddRangeAsync(entities);
            var resultSave = await testDatabase.SaveChangesAsync();

            var entityRemove = entities.Last();
            var resultRemove = testDatabaseTable.Remove(entityRemove);
            var resultAfterRemove = await testDatabase.SaveChangesAsync();
            var entityAfterRemove = await testDatabaseTable.ToListAsync();

            Assert.True(resultSave.OkStatus);
            Assert.True(resultRemove.OkStatus);
            Assert.True(resultAfterRemove.OkStatus);
            Assert.Equal(entities.Count - 1, entityAfterRemove.Value.Count);
        }

        /// <summary>
        /// Добавить сущности в таблицу. Удалить первую
        /// </summary>
        [Fact]
        public async Task AddRange_DeleteNotFound()
        {
            var testDatabase = GetTestEntityDatabase();
            var testDatabaseTable = testDatabase.TestTable;
            var entities = EntityData.GetTestEntity();

            await testDatabaseTable.AddRangeAsync(entities);
            var resultSave = await testDatabase.SaveChangesAsync();

            var genderRemove = new TestEntity(TestEnum.Third, "Third");
            var resultRemove = testDatabaseTable.Remove(genderRemove);
            var resultAfterRemove = await testDatabase.SaveChangesAsync();

            Assert.True(resultSave.OkStatus);
            Assert.True(resultRemove.OkStatus);
            Assert.True(resultAfterRemove.HasErrors);
            Assert.True(resultAfterRemove.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }

        /// <summary>
        /// База данных в памяти
        /// </summary>
        private static ITestDatabase GetTestEntityDatabase() =>
            new TestEntityDatabase(GetGetTestEntityDatabaseOptions());

        /// <summary>
        /// Параметры подключения к базе
        /// </summary>
        private static DbContextOptions GetGetTestEntityDatabaseOptions() =>
            new DbContextOptionsBuilder().
                UseInMemoryDatabase(Guid.NewGuid().ToString()).
                Options;
    }
}