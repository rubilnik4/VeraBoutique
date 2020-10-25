using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Преобразования доменной модели в модель базы данных
    /// </summary>
    public class TestEntityConverter : EntityConverter<TestEnum, ITestDomain, ITestEntity, TestEntity>, ITestEntityConverter
    {
        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override IResultValue<ITestDomain> FromEntity(ITestEntity testEntity) =>
            new TestDomain(testEntity.TestEnum, testEntity.Name).
            Map(test => new ResultValue<ITestDomain>(test));

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestEntity ToEntity(ITestDomain test) =>
            new TestEntity(test.TestEnum, test.Name);
    }
}