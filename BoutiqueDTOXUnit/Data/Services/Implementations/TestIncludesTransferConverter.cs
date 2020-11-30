using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTOXUnit.Data.Services.Implementations
{
    /// <summary>
    /// Тестовый конвертер включенных трансферных моделей
    /// </summary>
    public class TestIncludesTransferConverter : TransferConverter<string, ITestIncludeDomain, TestIncludeTransfer>, 
                                                 ITestIncludesTransferConverter
    {
        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override IResultValue<ITestIncludeDomain> FromTransfer(TestIncludeTransfer testIncludeTransfer) =>
            new TestIncludeDomain(testIncludeTransfer.Name).
            Map(testInclude => new ResultValue<ITestIncludeDomain>(testInclude));

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestIncludeTransfer ToTransfer(ITestIncludeDomain testDomain) =>
            new TestIncludeTransfer(testDomain.Name);
    }
}