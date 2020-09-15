using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Data.Models.Implementations;
using BoutiqueDTO.Data.Models.Interfaces;
using BoutiqueDTO.Data.Services.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;

namespace BoutiqueDTO.Data.Services.Implementations
{
    /// <summary>
    /// Преобразования доменной модели в модель базы данных
    /// </summary>
    public class TestTransferConverter : TransferConverter<TestEnum, ITestDomain, ITestTransfer>, ITestTransferConverter
    {
        /// <summary>
        /// Преобразовать из модели базы данных
        /// </summary>
        public override ITestDomain FromTransfer(ITestTransfer testTransfer) =>
            new TestDomain(testTransfer.TestEnum, testTransfer.Name);

        /// <summary>
        /// Преобразовать  в модель базы данных
        /// </summary>
        public override ITestTransfer ToTransfer(ITestDomain test) =>
            new TestTransfer(test.TestEnum, test.Name);
    }
}