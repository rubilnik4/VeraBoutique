using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public class GenderDatabaseTable: EntityDatabaseTable<GenderType, GenderEntity>, IGenderDatabaseTable
    {
        public GenderDatabaseTable(DbSet<GenderEntity> genderSet, string tableName)
            :base(genderSet, tableName)
        { }
    }
}