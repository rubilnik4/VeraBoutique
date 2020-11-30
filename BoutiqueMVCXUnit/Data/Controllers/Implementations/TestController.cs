using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVCXUnit.Data.Database.Interfaces;

namespace BoutiqueMVCXUnit.Data.Controllers.Implementations
{
    /// <summary>
    /// Тестовый контроллер
    /// </summary>
    public class TestController : ApiController<TestEnum, TestTransfer, ITestDomain>
    {
        public TestController(ITestDatabaseService testDatabaseService, ITestTransferConverter testTransferConverter)
            : base(testDatabaseService, testTransferConverter)
        { }
    }
}