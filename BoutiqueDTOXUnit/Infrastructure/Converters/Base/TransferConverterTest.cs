using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Data.Services.Implementations;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в трансферную модель. Тесты
    /// </summary>
    public class TransferConverterTest
    {
        /// <summary>
        /// Преобразования модели в трансферную модель
        /// </summary>
        [Fact]
        public void ToEntities_FromEntities()
        {
            var testDomains = TestData.TestDomains();
            var testEntityConverter = new TestTransferConverter();

            var testEntities = testEntityConverter.ToTransfers(testDomains);
            var testDomainsAfterConverter = testEntityConverter.FromTransfers(testEntities);

            Assert.True(testDomains.SequenceEqual(testDomainsAfterConverter));
        }
    }
}