using System;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public class TestDatabaseService : DatabaseService<TestEnum, ITestDomain, TestEntity>
    {
        public TestDatabaseService(IResultValue<IDatabase> database,
                                   IResultValue<IDatabaseTable<TestEnum, TestEntity>> testDatabaseTable,
                                   IEntityConverter<TestEnum, ITestDomain, TestEntity> testEntityConverter)
            : base(database, testDatabaseTable, testEntityConverter)
        { }

        /// <summary>
        /// Создать модель базы данных для удаления по идентификатору
        /// </summary>
        protected override TestEntity CreateRemoveEntityById(TestEnum id) =>
            new TestEntity(id, String.Empty);
    }
}