using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace MVCXUnit.Data.Database.Interfaces
{
    /// <summary>
    /// Тестовый сервис работы с базой данных
    /// </summary>
    public interface ITestDatabaseService : IDatabaseService<TestEnum, ITestDomain>
    {
        
    }
}