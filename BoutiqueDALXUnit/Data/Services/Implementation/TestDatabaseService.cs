using System;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public class TestDatabaseService : DatabaseService<TestEnum, ITestDomain, ITestEntity, TestEntity>
    {
        public TestDatabaseService(IDatabase database,
                                   IDatabaseTable<TestEnum, ITestDomain, TestEntity> testDatabaseTable,
                                   IEntityConverter<TestEnum, ITestDomain, ITestEntity, TestEntity> testEntityConverter)
            : base(database, testDatabaseTable, testEntityConverter)
        { }
    }
}