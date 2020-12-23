using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;

namespace BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api
{
    /// <summary>
    /// Тестовый Api сервис
    /// </summary>
    public interface ITestApiService: IApiService<TestEnum, TestTransfer>
    { }
}