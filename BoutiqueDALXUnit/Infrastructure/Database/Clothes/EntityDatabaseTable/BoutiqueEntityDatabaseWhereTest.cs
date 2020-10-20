using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable
{
    /// <summary>
    /// Тесты поиска элементов в базе данных
    /// </summary>
    public class BoutiqueEntityDatabaseWhereTest
    {
        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        public static async Task WhereById(IBoutiqueDatabase database, GenderType idWhere)
        {
            var genderGetEntity = await database.GendersTable.Where(idWhere).
                                                 AsNoTracking().FirstOrDefaultAsync();

            Assert.NotNull(genderGetEntity);
            Assert.True(genderGetEntity.GenderType == idWhere);
        }

        /// <summary>
        /// Получить сущности по идентификаторам
        /// </summary>
        public static async Task WhereByIds(IBoutiqueDatabase database, IReadOnlyCollection<GenderType> idsWhere)
        {
            var genderGetEntities = await database.GendersTable.Where(idsWhere).
                                                   AsNoTracking().ToListAsync();

            Assert.True(genderGetEntities.Select(entity => entity.GenderType).SequenceEqual(idsWhere));
        }

        /// <summary>
        /// Получить сущность по идентификатору с включением
        /// </summary>
        public static async Task WhereById_IncludeEntities(IBoutiqueDatabase database, GenderType idWhere)
        {
            var genderGetEntity = await database.GendersTable.
                Where<(string, GenderType), ClothesTypeGenderCompositeEntity>(idWhere, entity => entity.ClothesTypeGenderEntities).
                AsNoTracking().
                FirstOrDefaultAsync();

            Assert.True(genderGetEntity.GenderType == idWhere);
            Assert.True(genderGetEntity.ClothesTypeGenderEntities.All(entity => entity.GenderType == idWhere));
        }

        /// <summary>
        /// Получить сущность по идентификатору с включением
        /// </summary>
        public static async Task WhereByIds_IncludeEntities(IBoutiqueDatabase database, IReadOnlyCollection<GenderType> idsFind,
                                                           IReadOnlyCollection<ClothesTypeGenderCompositeEntity> clothesTypeGenderEntities)
        {
            var genderGetEntities = await database.GendersTable.
                Where<(string, GenderType), ClothesTypeGenderCompositeEntity>(idsFind, entity => entity.ClothesTypeGenderEntities).
                AsNoTracking().
                ToListAsync();

            Assert.True(genderGetEntities.Select(entity => entity.GenderType).SequenceEqual(idsFind));
            Assert.True(genderGetEntities.All(gender => gender.ClothesTypeGenderEntities.Count ==
                                                        clothesTypeGenderEntities.Count(entity => entity.GenderType == gender.GenderType)));
        }
    }
}