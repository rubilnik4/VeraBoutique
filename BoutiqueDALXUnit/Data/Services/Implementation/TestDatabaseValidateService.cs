using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Тестовый сервис проверки данных
    /// </summary>
    public class TestDatabaseValidateService : DatabaseValidateService<TestEnum, ITestDomain, TestEntity>,
                                               ITestDatabaseValidateService
    {
        public TestDatabaseValidateService(ITestTable testTable)
            : base(testTable)
        { }
    }
}