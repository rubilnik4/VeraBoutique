using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using BoutiqueXamarinCommon.Models.Implementation.Configuration;
using BoutiqueXamarinCommonXUnit.Data.Transfers.Configuration;
using BoutiqueXamarinCommonXUnit.Infrastructure.Mocks.Converters;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueXamarinCommonXUnit.Infrastructure.Converters
{
    /// <summary>
    /// Конвертер конфигурации Xamarin в трансферную модель. Тесты
    /// </summary>
    public class XamarinConfigurationTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var xamarinConfiguration = XamarinConfigurationData.XamarinConfigurationDomains.First();
            var xamarinConfigurationTransferConverter = XamarinConfigurationTransferConverterMock.XamarinConfigurationTransferConverter;

            var xamarinConfigurationTransfer = xamarinConfigurationTransferConverter.ToTransfer(xamarinConfiguration);
            var xamarinConfigurationAfterConverter = xamarinConfigurationTransferConverter.FromTransfer(xamarinConfigurationTransfer);

            Assert.True(xamarinConfigurationAfterConverter.OkStatus);
            Assert.True(xamarinConfiguration.Equals(xamarinConfigurationAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели в трансферную модель. Ошибка параметров подключения
        /// </summary>
        [Fact]
        public void ToTransfer_HostError()
        {
            var hostConfigurationNull = new XamarinConfigurationTransfer(null!);
            var xamarinConfigurationTransferConverter = XamarinConfigurationTransferConverterMock.XamarinConfigurationTransferConverter;

            var xamarinConfigurationAfterConverter = xamarinConfigurationTransferConverter.FromTransfer(hostConfigurationNull);

            Assert.True(xamarinConfigurationAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(xamarinConfigurationAfterConverter.Errors.First());
        }
    }
}