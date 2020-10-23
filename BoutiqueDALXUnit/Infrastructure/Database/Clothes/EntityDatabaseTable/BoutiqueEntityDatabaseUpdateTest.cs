using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using Functional.Models.Enums;
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
        public static async Task Update(IBoutiqueDatabase database, GenderType idUpdate)
        {
            var genderFind = await database.GendersTable.FindAsync(idUpdate);
            var genderUpdate = new GenderEntity(genderFind.Value.GenderType, "Куропатка");

            var resultUpdate = database.GendersTable.Update(genderUpdate);
            var resultSave = await database.SaveChangesAsync();
            var genderGet = await database.GendersTable.FindAsync(genderUpdate.GenderType);

            Assert.True(resultUpdate.OkStatus);
            Assert.True(resultSave.OkStatus);
            Assert.True(genderGet.OkStatus);
            Assert.Equal(genderUpdate.Name, genderGet.Value.Name);
        }

        /// <summary>
        /// Обновить элемент в базе. Запись не найдена
        /// </summary>
        public static async Task Update_NotFound(IBoutiqueDatabase database)
        {
            var clothesTypeUpdate = new ClothesTypeEntity("NotFound", new CategoryEntity("NotFound"));

            var resultUpdate = database.ClotheTypeTable.Update(clothesTypeUpdate);
            var resultSave = await database.SaveChangesAsync();
            database.Detach();

            Assert.True(resultUpdate.OkStatus);
            Assert.True(resultSave.HasErrors);
            Assert.True(resultSave.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }
    }
}