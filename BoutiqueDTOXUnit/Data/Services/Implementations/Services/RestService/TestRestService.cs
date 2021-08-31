using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.RestService;

namespace BoutiqueDTOXUnit.Data.Services.Implementations.Services.RestService
{
    /// <summary>
    /// Тестовый сервис для данных Api
    /// </summary>
    public class TestRestService : RestServiceBase<TestEnum, ITestDomain, TestTransfer>, ITestRestService
    {
        public TestRestService(IRestHttpClient restHttpClient, ITestTransferConverter testTransferConverter)
            : base(restHttpClient, testTransferConverter)
        { }
    }
}