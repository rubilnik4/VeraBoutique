using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDALXUnit.Data.Models.Implementation;

namespace BoutiqueDALXUnit.Data.Database.Interfaces
{
    /// <summary>
    /// Тестовая таблица включенных записей базы данных
    /// </summary>
    public interface ITestIncludeDatabaseTable : IDatabaseTable<string, TestIncludeEntity>
    { }
}