using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable
{
    /// <summary>
    /// Тесты удаления элементов в базе данных
    /// </summary>
    public static class BoutiqueEntityDatabaseDeleteTest
    {
        /// <summary>
        /// Удаление сущности из таблицы
        /// </summary>
        public static async Task Delete(IBoutiqueDatabase database, IReadOnlyCollection<GenderEntity> genderEntities)
        {
            foreach(var gender in genderEntities)
            {
                var genderFind = await database.GendersTable.FindAsync(gender.Id);
                var resultRemove = database.GendersTable.Remove(genderFind.Value);
            }
            var resultAfterRemove = await database.SaveChangesAsync();
            var entitiesAfterRemove = await database.GendersTable.ToListAsync();
            database.Detach();

            Assert.True(resultAfterRemove.OkStatus);
            Assert.True(entitiesAfterRemove.Value.Count == 0);
        }

        /// <summary>
        /// Удалить все записи в таблице
        /// </summary>
        public static async Task Delete_All(IBoutiqueDatabase database, IReadOnlyCollection<GenderEntity> genderEntities,
                                            IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                            IReadOnlyCollection<ClothesTypeGenderEntity> clothesTypeGenderEntities)
        {
            var resultClothesRemove = database.ClotheTypeTable.RemoveRange(clothesTypeEntities);
            var resultAfterRemove = await database.SaveChangesAsync();
            var entitiesAfterRemove = await database.ClotheTypeTable.ToListAsync();
            database.Detach();

            Assert.True(resultClothesRemove.OkStatus);
            Assert.True(resultAfterRemove.OkStatus);
            Assert.True(entitiesAfterRemove.Value.Count == 0);
        }
    }
}