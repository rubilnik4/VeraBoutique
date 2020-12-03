using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDALXUnit.Data.Services.Interfaces
{
    /// <summary>
    /// Тестовый сервис проверки вложенных данных
    /// </summary>
    public interface ITestIncludeDatabaseValidateService : IDatabaseValidateService<string, ITestIncludeDomain>
    { }
}