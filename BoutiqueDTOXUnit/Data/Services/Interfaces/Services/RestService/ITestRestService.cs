using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTOXUnit.Data.Services.Interfaces.Services.RestService
{
    /// <summary>
    /// Тестовый сервис для данных Api
    /// </summary>
    public interface ITestRestService : IRestServiceBase<TestEnum, ITestDomain>
    { }
}