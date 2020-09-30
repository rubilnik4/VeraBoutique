using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Enums;
using Xunit;
using static BoutiqueDALXUnit.Infrastructure.Database.Base.BoutiqueEntityDatabaseTest;

namespace BoutiqueDALXUnit.Infrastructure.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Тесты
    /// </summary>
    public static class BoutiqueEntityDatabaseGetTest
    {
        /// <summary>
        /// Добавить сущности в таблицу. Проверить идентификаторы
        /// </summary>
        public static async Task AddRangeEntities(IBoutiqueDatabase database, IList<GenderEntity> genderEntities, 
                                                  IList<ClothesTypeEntity> clothesTypeEntities)
        {
            var genderEntitiesWithClothesType = EntityData.GetGenderEntitiesWithClothesType(genderEntities, clothesTypeEntities);

            var clothesIds = await database.ClotheTypeTable.AddRangeAsync(clothesTypeEntities);
            var genderIds = await database.GendersTable.AddRangeAsync(genderEntitiesWithClothesType);
            var result = await database.SaveChangesAsync();

            Assert.True(result.OkStatus);
            Assert.True(genderIds.OkStatus);
            Assert.True(clothesIds.OkStatus);
            Assert.True(genderIds.Value.SequenceEqual(genderEntities.Select(entity => entity.Id)));
            Assert.True(clothesIds.Value.SequenceEqual(clothesTypeEntities.Select(entity => entity.Id)));
        }

        /// <summary>
        /// Получить сущности из таблицы
        /// </summary>
        public static async Task ToListEntities(IBoutiqueDatabase database, IList<GenderEntity> genderEntities, 
                                                IList<ClothesTypeEntity> clothesTypeEntities)
        {
            var clothesTypeGetEntities = await database.ClotheTypeTable.ToListAsync();
            var genderGetEntities = await database.GendersTable.
                                    ToListAsync<(string, GenderType), ClothesTypeGenderEntity>(gender => gender.ClothesTypeGenderEntities);
            
            Assert.True(genderGetEntities.OkStatus);
            Assert.True(clothesTypeGetEntities.OkStatus);
            Assert.True(genderEntities.SequenceEqual(genderGetEntities.Value));
            Assert.True(clothesTypeEntities.SequenceEqual(clothesTypeGetEntities.Value));
            Assert.True(genderGetEntities.Value.All(gender => gender.ClothesTypeGenderEntities.Count == clothesTypeEntities.Count));
        }

        ///// <summary>
        ///// Добавить одинаковые данные дважды. Ошибка
        ///// </summary>
        //[Fact]
        //public async Task AddRange_DuplicateError()
        //{
        //    var testDatabase = GetTestEntityDatabase();
        //    var testDatabaseTable = testDatabase.TestTable;
        //    var entities = EntityData.TestEntities;

        //    await testDatabaseTable.AddRangeAsync(entities);
        //    var firstResult = await testDatabase.SaveChangesAsync();

        //    await testDatabaseTable.AddRangeAsync(entities);
        //    var secondResult = await testDatabase.SaveChangesAsync();

        //    Assert.True(firstResult.OkStatus);
        //    Assert.True(secondResult.HasErrors);
        //    Assert.True(secondResult.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        //}


    }
}