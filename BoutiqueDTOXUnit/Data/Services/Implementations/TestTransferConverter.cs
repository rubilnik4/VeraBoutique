using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces;

namespace BoutiqueDTOXUnit.Data.Services.Implementations
{
    /// <summary>
    /// Преобразования доменной модели в модель базы данных
    /// </summary>
    public class TestTransferConverter : TransferConverter<TestEnum, ITestDomain, TestTransfer>, ITestTransferConverter
    {
        public TestTransferConverter(ITestIncludesTransferConverter testIncludesTransferConverter)
        {
            _testIncludesTransferConverter = testIncludesTransferConverter;
        }

        /// <summary>
        /// Тестовый конвертер включенных трансферных моделей
        /// </summary>
        private readonly ITestIncludesTransferConverter _testIncludesTransferConverter;

        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override ITestDomain FromTransfer(TestTransfer testTransfer) =>
            new TestDomain(testTransfer,
                           _testIncludesTransferConverter.FromTransfers(testTransfer.TestIncludes));

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override TestTransfer ToTransfer(ITestDomain test) =>
            new TestTransfer(test, _testIncludesTransferConverter.ToTransfers(test.TestIncludes));
    }
}