using System.ComponentModel.Design;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api;
using RestSharp;

namespace BoutiqueDTOXUnit.Data.Services.Implementations.Services.Api
{
    /// <summary>
    /// Тестовый Api сервис
    /// </summary>
    public class TestApiService: ApiService<TestEnum, TestTransfer>, ITestApiService
    {
        public TestApiService(IRestClient restClient)
            :base(restClient)
        { }
    }
}