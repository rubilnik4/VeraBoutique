using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Data.Models.Interfaces;
using BoutiqueDTO.Data.Services.Interfaces;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVCXUnit.Data.Database.Interfaces;

namespace BoutiqueMVCXUnit.Data.Controllers.Implementations
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