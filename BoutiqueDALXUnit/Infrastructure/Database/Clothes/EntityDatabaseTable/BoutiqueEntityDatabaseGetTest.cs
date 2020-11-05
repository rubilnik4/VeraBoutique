using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
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
                                                IReadOnlyCollection<ClothesTypeFullEntity> clothesTypeEntities,
                                                IReadOnlyCollection<ClothesTypeGenderCompositeEntity> clothesTypeGenderEntities)
        {
            var clothesTypeGetEntities = await database.ClotheTypeTable.ToListAsync();
            var genderGetEntities = await database.GendersTable.
                                    ToListAsync<(string, GenderType), ClothesTypeGenderCompositeEntity>(gender => gender.ClothesTypeGenderComposites);

            Assert.True(genderGetEntities.OkStatus);
            Assert.True(clothesTypeGetEntities.OkStatus);
            Assert.True(genderEntities.SequenceEqual(genderGetEntities.Value));
            Assert.True(clothesTypeEntities.SequenceEqual(clothesTypeGetEntities.Value));
            Assert.True(genderGetEntities.Value.All(gender => gender.ClothesTypeGenderComposites?.Count ==
                                                              clothesTypeGenderEntities.Count(entity => entity.GenderType == gender.GenderType)));
        }

    }
}