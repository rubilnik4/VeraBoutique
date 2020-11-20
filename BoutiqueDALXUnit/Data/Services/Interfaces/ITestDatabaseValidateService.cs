using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDALXUnit.Data.Services.Interfaces
{
    /// <summary>
    /// Тестовый сервис проверки данных
    /// </summary>
    public interface ITestDatabaseValidateService : IDatabaseValidateService<TestEnum, ITestDomain>
    { }
}