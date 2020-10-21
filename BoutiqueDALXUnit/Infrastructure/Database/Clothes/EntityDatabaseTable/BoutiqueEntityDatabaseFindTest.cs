using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable
{
    /// <summary>
    /// Тесты поиска элементов в базе данных
    /// </summary>
    public static class BoutiqueEntityDatabaseFindTest
    {
        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        public static async Task FindById(IBoutiqueDatabase database, GenderType idFind)
        {
            var genderGetEntity = await database.GendersTable.FindAsync(idFind);

            Assert.True(genderGetEntity.OkStatus);
            Assert.True(genderGetEntity.Value.GenderType == idFind);
        }

        /// <summary>
        /// Получить сущности по идентификаторам
        /// </summary>
        public static async Task FindByIds(IBoutiqueDatabase database, IReadOnlyCollection<GenderType> idsFind)
        {
            var genderGetEntities = await database.GendersTable.FindAsync(idsFind);

            Assert.True(genderGetEntities.OkStatus);
            Assert.True(genderGetEntities.Value.Select(entity => entity.GenderType).SequenceEqual(idsFind));
        }

        /// <summary>
        /// Получить сущность по идентификатору с включением
        /// </summary>
        public static async Task FindById_IncludeEntities(IBoutiqueDatabase database, GenderType idFind)
        {
            var genderGetEntity = await database.GendersTable.
                FindAsync<(string, GenderType), ClothesTypeGenderCompositeEntity>(idFind, entity => entity.ClothesTypeGenderEntities);

            Assert.True(genderGetEntity.OkStatus);
            Assert.True(genderGetEntity.Value.GenderType == idFind);
            Assert.True(genderGetEntity.Value.ClothesTypeGenderEntities.All(entity => entity.GenderType == idFind));
        }

        /// <summary>
        /// Получить сущность по идентификатору с включением
        /// </summary>
        public static async Task FindByIds_IncludeEntities(IBoutiqueDatabase database, IReadOnlyCollection<GenderType> idsFind,
                                                           IReadOnlyCollection<ClothesTypeGenderCompositeEntity> clothesTypeGenderEntities)
        {
            var genderGetEntities = await database.GendersTable.
                FindAsync<(string, GenderType), ClothesTypeGenderCompositeEntity>(idsFind, entity => entity.ClothesTypeGenderEntities);

            Assert.True(genderGetEntities.OkStatus);
            Assert.True(genderGetEntities.Value.Select(entity => entity.GenderType).SequenceEqual(idsFind));
            Assert.True(genderGetEntities.Value.All(gender => gender.ClothesTypeGenderEntities.Count ==
                                                              clothesTypeGenderEntities.Count(entity => entity.GenderType == gender.GenderType)));
        }

        /// <summary>
        /// Получить сущность по идентификатору. Элемент не найден
        /// </summary>
        public static async Task FindById_NotFound(IBoutiqueDatabase database)
        {
            var genderGetEntity = await database.ClotheTypeTable.FindAsync("NotFound");

            Assert.True(genderGetEntity.HasErrors);
            Assert.True(genderGetEntity.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Получить сущности по идентификаторам. Добавочные элементы не найдены
        /// </summary>
        public static async Task FindByIds_NotFound(IBoutiqueDatabase database, IReadOnlyCollection<ClothesTypeEntity> clothesTypeEntities)
        {
            var testFind = await database.ClotheTypeTable.FindAsync(new List<string> {"NotFound", clothesTypeEntities .First().Name});

            Assert.True(testFind.OkStatus);
            Assert.True(testFind.Value.Count == 1);
        }
    }
}