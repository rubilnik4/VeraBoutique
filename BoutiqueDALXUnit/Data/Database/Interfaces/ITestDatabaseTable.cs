using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Models.Implementation;

namespace BoutiqueDALXUnit.Data.Database.Interfaces
{
    /// <summary>
    /// Тестовая таблица базы данных
    /// </summary>
    public interface ITestDatabaseTable: IDatabaseTable<TestEnum, TestEntity>
    { }
}