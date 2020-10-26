using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Data.Models.Interfaces;
using BoutiqueDTO.Data.Services.Interfaces;
using BoutiqueMVC.Controllers.Implementations.Base;
using MVCXUnit.Data.Database.Interfaces;

namespace MVCXUnit.Data.Controllers.Implementations
{
    /// <summary>
    /// Тестовый контроллер
    /// </summary>
    public class TestController : ApiController<TestEnum, ITestTransfer, ITestDomain>
    {
        public TestController(ITestDatabaseService testDatabaseService, ITestTransferConverter testTransferConverter)
            : base(testDatabaseService, testTransferConverter)
        { }
    }
}