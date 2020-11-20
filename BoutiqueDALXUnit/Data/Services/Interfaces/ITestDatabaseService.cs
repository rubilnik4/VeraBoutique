using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDALXUnit.Data.Services.Interfaces
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public interface ITestDatabaseService: IDatabaseService<TestEnum, ITestDomain>
    { }
}