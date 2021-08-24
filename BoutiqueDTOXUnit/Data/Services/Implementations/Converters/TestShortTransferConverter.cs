using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTOXUnit.Data.Services.Implementations.Converters
{
    /// <summary>
    /// Тестовый конвертер основных данных трансферных моделей
    /// </summary>
    public class TestShortTransferConverter: TransferConverter<TestEnum, ITestShortDomain, TestShortTransfer>, 
                                             ITestShortTransferConverter
    {
        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override IResultValue<ITestShortDomain> FromTransfer(TestShortTransfer testShortTransfer) =>
            new TestShortDomain(testShortTransfer.TestEnum, testShortTransfer.Name).
            Map(testShort => new ResultValue<ITestShortDomain>(testShort));

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestShortTransfer ToTransfer(ITestShortDomain test) =>
            new TestShortTransfer(test.TestEnum, test.Name);
    }
}