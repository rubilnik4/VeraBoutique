using System;
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
    /// Тесты удаления элементов в базе данных
    /// </summary>
    public static class BoutiqueEntityDatabaseDeleteTest
    {
        /// <summary>
        /// Добавить сущности в таблицу. Удалить первую
        /// </summary>
     
        public static async Task Delete(IBoutiqueDatabase database, GenderEntity entityDelete)
        {
            var resultRemove = database.GendersTable.Remove(entityDelete);
            var resultAfterRemove = await database.SaveChangesAsync();
            var entityAfterRemove = await database.GendersTable.ToListAsync();

            Assert.True(resultRemove.OkStatus);
            Assert.True(resultAfterRemove.OkStatus);
            Assert.True(entityAfterRemove.Value.All(entity => entity.GenderType != entityDelete.GenderType));
        }

    }
}