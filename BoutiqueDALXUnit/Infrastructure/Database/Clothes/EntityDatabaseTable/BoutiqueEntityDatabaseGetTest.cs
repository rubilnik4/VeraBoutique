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
    /// Тесты добавления и получения элементов в базе данных
    /// </summary>
    public static class BoutiqueEntityDatabaseGetTest
    {
        /// <summary>
        /// Добавить сущности в таблицу. Проверить идентификаторы
        /// </summary>
        public static async Task AddRangeEntities(IBoutiqueDatabase database)
        {
            //var clothesTypeGenderEntities = new List<ClothesTypeGenderEntity>
            //{
            //    new ClothesTypeGenderEntity("First", GenderType.Male),
            //    new ClothesTypeGenderEntity("Second", GenderType.Female),
            //};
            //var clothesTypeEntities = new List<ClothesTypeEntity>
            //{
            //    new ClothesTypeEntity("First", clothesTypeGenderEntities.ToList()),
            //    new ClothesTypeEntity("Second", clothesTypeGenderEntities.ToList()),
            //};

            //var clothesIds = await database.ClotheTypeTable.AddRangeAsync(clothesTypeEntities);
            //var result = await database.SaveChangesAsync();
            //var getClothesTypes = await database.ClotheTypeTable.ToListAsync<(string, GenderType), ClothesTypeGenderEntity>(entity => entity.ClothesTypeGenderEntities);

            //Assert.True(result.OkStatus);
            //Assert.True(clothesIds.OkStatus);
            //Assert.True(clothesIds.Value.SequenceEqual(clothesTypeEntities.Select(entity => entity.Id)));
            //Assert.True(getClothesTypes.Value.
            //            Where(entity => clothesTypeEntities.Contains(entity)).
            //            All(entity => entity.ClothesTypeGenderEntities.Count > 0));
        }

        /// <summary>
        /// Добавить одинаковые данные дважды. Ошибка
        /// </summary>
        public static async Task AddRange_DuplicateError(IBoutiqueDatabase database, IReadOnlyCollection<GenderEntity> genderEntities)
        {
            var genderIds = await database.GendersTable.AddRangeAsync(genderEntities);
            var resultSave = await database.SaveChangesAsync();
            database.Detach();

            Assert.True(genderIds.OkStatus);
            Assert.True(resultSave.HasErrors);
            Assert.True(resultSave.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }

        /// <summary>
        /// Получить сущности из таблицы
        /// </summary>
        public static async Task ToListEntities(IBoutiqueDatabase database, IReadOnlyCollection<GenderEntity> genderEntities,
                                                IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities,
                                                IReadOnlyCollection<ClothesTypeGenderEntity> clothesTypeGenderEntities)
        {
            var clothesTypeGetEntities = await database.ClotheTypeTable.ToListAsync();
            var genderGetEntities = await database.GendersTable.
                                    ToListAsync<(string, GenderType), ClothesTypeGenderEntity>(gender => gender.ClothesTypeGenderEntities);

            Assert.True(genderGetEntities.OkStatus);
            Assert.True(clothesTypeGetEntities.OkStatus);
            Assert.True(genderEntities.SequenceEqual(genderGetEntities.Value));
            Assert.True(clothesTypeEntities.SequenceEqual(clothesTypeGetEntities.Value));
            Assert.True(genderGetEntities.Value.All(gender => gender.ClothesTypeGenderEntities.Count ==
                                                              clothesTypeGenderEntities.Count(entity => entity.GenderTypeId == gender.GenderType)));
        }

    }
}