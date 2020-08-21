using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueEF.Entities.Clothes;
using BoutiqueEF.Fabric;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services
{
    public class GenderServiceEfTest
    {
        [Fact]
        public async Task Test()
        {
            var options = new DbContextOptionsBuilder<BoutiqueDatabase>().
                          UseInMemoryDatabase(Guid.NewGuid().ToString()).
                          Options;
            var context = new BoutiqueDatabase(options);

            var genders = GetGenders();
            await context.Genders.AddRangeAsync(GetGenders());

            await context.SaveChangesAsync();

            var gendersDb = context.Genders.ToListAsync();
        }

        /// <summary>
        /// Получить типы полов
        /// </summary>
        private static IReadOnlyCollection<GenderEntity> GetGenders() =>
            new List<GenderEntity>()
            {
                new GenderEntity(){GenderType = GenderType.Male, Name = "Мужик"},
                new GenderEntity(){GenderType = GenderType.Female, Name = "Тетя" },
            };
    }
}