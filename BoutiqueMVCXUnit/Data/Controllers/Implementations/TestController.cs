using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using BoutiqueMVC.Controllers.Base;
using BoutiqueMVCXUnit.Data.Database.Interfaces;

namespace BoutiqueMVCXUnit.Data.Controllers.Implementations
{
    /// <summary>
    /// Тестовый контроллер
    /// </summary>
    public class TestController : ApiController<TestEnum, ITestDomain, TestTransfer>
    {
        public TestController(ITestDatabaseService testDatabaseService, ITestTransferConverter testTransferConverter)
            : base(testDatabaseService, testTransferConverter)
        { }
    }
}