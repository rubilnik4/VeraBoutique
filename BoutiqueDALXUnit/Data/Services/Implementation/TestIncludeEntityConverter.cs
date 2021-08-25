using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDALXUnit.Data.Services.Implementation
{
    /// <summary>
    /// Преобразования доменной модели в модель базы данных
    /// </summary>
    public class TestIncludeEntityConverter : EntityConverter<string, ITestIncludeDomain, TestIncludeEntity>, ITestIncludeEntityConverter
    {
        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override IResultValue<ITestIncludeDomain> FromEntity(TestIncludeEntity testIncludeEntity) =>
            new TestIncludeDomain(testIncludeEntity).
            Map(testInclude => new ResultValue<ITestIncludeDomain>(testInclude));

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestIncludeEntity ToEntity(ITestIncludeDomain testInclude) =>
            new TestIncludeEntity(testInclude);
    }
}