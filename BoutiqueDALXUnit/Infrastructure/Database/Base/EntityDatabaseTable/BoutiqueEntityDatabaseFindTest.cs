using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Enums;
using Xunit;
using static BoutiqueDALXUnit.Infrastructure.Database.Base.BoutiqueEntityDatabaseTest;

namespace BoutiqueDALXUnit.Infrastructure.Database.Base.EntityDatabaseTable
{
    public class BoutiqueEntityDatabaseFindTest
    {

        ///// <summary>
        ///// Добавить сущности в таблицу. Получить вторую
        ///// </summary>
        //[Fact]
        //public async Task AddRange_FindId_Second()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    var ids = await testDatabaseTable.AddRangeAsync(entities);
        //    var result = await testDatabase.SaveChangesAsync();
        //    var getId = ids.Value.Last();
        //    var entityGet = await testDatabaseTable.FindAsync(getId);

        //    Assert.True(result.OkStatus);
        //    Assert.True(entityGet.OkStatus);
        //    Assert.True(entityGet.Value.Equals(entities.First(entity => entity.TestEnum == getId)));
        //}

        ///// <summary>
        ///// Добавить сущности в таблицу. Элемент не найден
        ///// </summary>
        //[Fact]
        //public async Task AddRange_FindId_NotFound()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    await testDatabaseTable.AddRangeAsync(entities);
        //    var result = await testDatabase.SaveChangesAsync();
        //    var genderGet = await testDatabaseTable.FindAsync(TestEnum.Third);

        //    Assert.True(result.OkStatus);
        //    Assert.True(genderGet.HasErrors);
        //    Assert.True(genderGet.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        //}

        ///// <summary>
        ///// Добавить сущности в таблицу. Получить вторую с включением сущностей
        ///// </summary>
        //[Fact]
        //public async Task AddRange_FindId_IncludeEntities()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var testIncludeDatabaseTable = testDatabase.TestIncludeTable;
        //    var includeEntities = EntityData.TestIncludeEntities;
        //    var entities = EntityData.GetTestEntitiesWithIncludes(includeEntities);

        //    await testIncludeDatabaseTable.AddRangeAsync(includeEntities);
        //    var ids = await testDatabaseTable.AddRangeAsync(entities);
        //    var result = await testDatabase.SaveChangesAsync();
        //    var getId = ids.Value.Last();
        //    var entityGet = await testDatabaseTable.FindAsync<string, TestIncludeEntity>(getId,
        //                                                                                 entity => entity.TestIncludeEntities);

        //    Assert.True(result.OkStatus);
        //    Assert.True(entityGet.OkStatus);
        //    Assert.True(entityGet.Value.Equals(entities.First(entity => entity.TestEnum == getId)));
        //    Assert.True(entityGet.Value.TestIncludeEntities.SequenceEqual(includeEntities));
        //}

        ///// <summary>
        ///// Добавить сущности в таблицу. Найти их по идентификаторам
        ///// </summary>
        //[Fact]
        //public async Task AddRange_FindIds()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    var ids = await testDatabaseTable.AddRangeAsync(entities);
        //    var result = await testDatabase.SaveChangesAsync();
        //    var testFind = await testDatabaseTable.FindAsync(ids.Value);

        //    Assert.True(result.OkStatus);
        //    Assert.True(testFind.OkStatus);
        //    Assert.True(testFind.Value.SequenceEqual(entities));
        //}


        ///// <summary>
        ///// Добавить сущности в таблицу. Найти их по идентификаторам с включением сущностей
        ///// </summary>
        //[Fact]
        //public async Task AddRange_FindIds_IncludeEntities()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var testIncludeDatabaseTable = testDatabase.TestIncludeTable;
        //    var entities = EntityData.TestEntities;

        //    var ids = await testDatabaseTable.AddRangeAsync(entities);
        //    var includeEntities = EntityData.TestEntities.
        //                          SelectMany(testEntity => EntityData.TestIncludeEntities.
        //                                                   Select(testIncludeEntity => new TestIncludeEntity(testIncludeEntity.Name, testEntity))).
        //                          ToList();
        //    await testIncludeDatabaseTable.AddRangeAsync(includeEntities);
          
        //    var result = await testDatabase.SaveChangesAsync();
        //    var testFind = await testDatabaseTable.FindAsync<string, TestIncludeEntity>(ids.Value,
        //                                                                                entity => entity.TestIncludeEntities);
        //    var testFind1 = await testIncludeDatabaseTable.FindAsync<TestEnum, TestEntity>(ids.Value,
        //                                                                                entity => entity.TestEntity);

        //    Assert.True(result.OkStatus);
        //    Assert.True(testFind.OkStatus);
        //    Assert.True(testFind.Value.SequenceEqual(entities));
        //    Assert.True(testFind.Value.First().TestIncludeEntities.SequenceEqual(includeEntities));
        //}

        ///// <summary>
        ///// Добавить сущности в таблицу. Отсутствие в базе
        ///// </summary>
        //[Fact]
        //public async Task AddRange_FindIds_NotFound()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = TestData.GetTestDomains();
        //    var ids = TestData.GetTestIds(entities);

        //    var testFind = await testDatabaseTable.FindAsync(ids);

        //    Assert.True(testFind.OkStatus);
        //    Assert.True(testFind.Value.Count == 0);
        //}
    }
}