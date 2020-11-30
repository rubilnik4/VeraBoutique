using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTOXUnit.Data.Services.Implementations
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