using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable
{
    /// <summary>
    /// Тесты обновления элементов в базе данных
    /// </summary>
    public static class BoutiqueEntityDatabaseUpdateTest
    {
        /// <summary>
        /// Обновить записи в таблице
        /// </summary>
        public static async Task Update(IBoutiqueDatabase database)
        {
            var genderEntity = new GenderEntity(GenderType.Child, "Дите");

            await database.GendersTable.AddRangeAsync(new List<GenderEntity>() { genderEntity });
            var resultAddSave = await database.SaveChangesAsync();

            var genderUpdate = new GenderEntity(genderEntity.GenderType, "Куропатка");

            var resultUpdate = database.GendersTable.Update(genderUpdate);
            var resultSave = await database.SaveChangesAsync();
            var genderGet = await database.GendersTable.FindAsync(genderUpdate.GenderType);

            Assert.True(resultUpdate.OkStatus);
            Assert.True(resultSave.OkStatus);
            Assert.True(genderGet.OkStatus);
            Assert.Equal(genderUpdate.Name, genderGet.Value.Name);
        }

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
    }
}