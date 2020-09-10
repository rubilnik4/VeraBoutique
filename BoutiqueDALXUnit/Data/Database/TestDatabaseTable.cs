using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDALXUnit.Data.Database
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public class TestDatabaseTable: EntityDatabaseTable<TestEnum, TestEntity>, 
                                      IGenderDatabaseTable<TestEnum, TestEntity>
    {
        public TestDatabaseTable(DbSet<TestEntity> testSet, string tableName)
            :base(testSet, tableName)
        { }
    }
}