using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDALXUnit.Data;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Factories.Database.Boutique
{
    public class BoutiqueDatabaseTest
    {
        [Fact]
        public async Task AddRange_ToList_Return()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
            var genders = EntityData.GetGenderEntities();

            await boutiqueDatabase.Genders.AddRangeAsync(genders);
            var result = await boutiqueDatabase.SaveChangesAsync();
            var gendersFromDb = await boutiqueDatabase.Genders.ToListAsync();

            Assert.True(result.OkStatus);
            Assert.True(gendersFromDb.CompareByFunc(genders, (genderFromDb, gender) => genderFromDb.EqualEntity(gender)));
        }
        [Fact]
        public async Task AddRange_DuplicateError()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
            var genders = EntityData.GetGenderEntities();

            await boutiqueDatabase.Genders.AddRangeAsync(genders);
            var firstResult = await boutiqueDatabase.SaveChangesAsync();

            await boutiqueDatabase.Genders.AddRangeAsync(genders);
            var secondResult = await boutiqueDatabase.SaveChangesAsync();

            Assert.True(firstResult.OkStatus);
            Assert.True(secondResult.HasErrors);
            Assert.True(secondResult.Errors.First().ErrorResultType == ErrorResultType.DatabaseSave);
        }

        /// <summary>
        /// База данных в памяти
        /// </summary>
        private static BoutiqueEntityDatabase GetBoutiqueDatabase() =>
            new BoutiqueEntityDatabase(GetBoutiqueDatabaseOptions());

        /// <summary>
        /// Параметры подключения к базе
        /// </summary>
        private static DbContextOptions GetBoutiqueDatabaseOptions() =>
            new DbContextOptionsBuilder().
            UseInMemoryDatabase(Guid.NewGuid().ToString()).
            Options;
    }
}