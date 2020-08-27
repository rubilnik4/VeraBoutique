using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Boutique;
using BoutiqueDALXUnit.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Factories.Database.Boutique
{
    public class GenderServiceTest
    {
        [Fact]
        public async Task Test()
        {
            var boutiqueDatabase = GetBoutiqueDatabase();
            var genders = EntityData.GetGenderEntities();

            await boutiqueDatabase.Genders.AddRangeAsync(genders);
            await boutiqueDatabase.SaveChangesAsync();
            var gendersFromDb = await boutiqueDatabase.Genders.ToListAsync();

            Assert.True(gendersFromDb.CompareByFunc(genders, (genderFromDb, gender) => genderFromDb.EqualEntity(gender)));
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