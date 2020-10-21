using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Database.Clothes.EntityDatabaseTable
{
    /// <summary>
    /// Тесты выбора элементов в базе данных
    /// </summary>
    public class BoutiqueEntityDatabaseSelectTest
    {
        /// <summary>
        /// Получить сущности с включением смежных
        /// </summary>
        public static async Task Select(IBoutiqueDatabase database)
        {
            var genderGetEntity = await database.GendersTable.
                Select(entity => new GenderEntity(entity.GenderType, entity.Name)).
                AsNoTracking().
                FirstOrDefaultAsync();

            Assert.NotNull(genderGetEntity);
        }
    }
}