using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.CommonErrors;
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
        public void ToTransfer_FromTransfer()
        {
            var testDomains = TestData.TestDomains;
            var testEntityConverter = TestTransferConverterMock.TestTransferConverter;

            var testEntities = testEntityConverter.ToTransfers(testDomains);
            var testDomainsAfterConverter = testEntityConverter.FromTransfers(testEntities);

            Assert.True(testDomainsAfterConverter.OkStatus);
            Assert.True(testDomains.SequenceEqual(testDomainsAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования в доменную модель
        /// </summary>
        [Fact]
        public void GetDomain_Ok()
        {
            var testTransfer = TestTransferData.TestTransfers.First();
            var testEntityConverter = TestTransferConverterMock.TestTransferConverter;

            var testDomainAfterConverter = testEntityConverter.GetDomain(testTransfer);

            Assert.True(testDomainAfterConverter.OkStatus);
            Assert.True(TestData.TestDomains.First().Equals(testDomainAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования в доменную модель. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void GetDomain_NullValue()
        {
            var testEntityConverter = TestTransferConverterMock.TestTransferConverter;

            var testDomainAfterConverter = testEntityConverter.GetDomain(null);

            Assert.True(testDomainAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(testDomainAfterConverter);
        }

        /// <summary>
        /// Преобразования в доменную модель
        /// </summary>
        [Fact]
        public void GetDomains_Ok()
        {
            var testTransfers = TestTransferData.TestTransfers;
            var testEntityConverter = TestTransferConverterMock.TestTransferConverter;

            var testDomainsAfterConverter = testEntityConverter.GetDomains(testTransfers);

            Assert.True(testDomainsAfterConverter.OkStatus);
            Assert.True(TestData.TestDomains.SequenceEqual(testDomainsAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования в доменную модель. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void GetDomains_NullValue()
        {
            var testEntityConverter = TestTransferConverterMock.TestTransferConverter;

            var testDomainAfterConverter = testEntityConverter.GetDomains(null);

            Assert.True(testDomainAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(testDomainAfterConverter);
        }

        /// <summary>
        /// Преобразования в доменную модель. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void GetDomains_NullCollection()
        {
            var testTransfers = TestTransferData.TestTransfers.Append(null!).ToList();
            var testEntityConverter = TestTransferConverterMock.TestTransferConverter;

            var testDomainAfterConverter = testEntityConverter.GetDomains(testTransfers!);

            Assert.True(testDomainAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(testDomainAfterConverter);
        }
    }
}