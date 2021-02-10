using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueDTOXUnit.Data.Transfers.Configuration;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Configuration
{
    /// <summary>
    /// Конвертер параметров подключения в трансферную модель. Тесты
    /// </summary>
    public class HostConfigurationTransferConverterTest
    {
        /// <summary>
        /// Преобразования параметров подключения в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var hostConfiguration = HostConfigurationData.HostConfigurationDomains.First();
            var hostConfigurationTransferConverter = new HostConfigurationTransferConverter();

            var hostConfigurationTransfer = hostConfigurationTransferConverter.ToTransfer(hostConfiguration);
            var hostConfigurationAfterConverter = hostConfigurationTransferConverter.FromTransfer(hostConfigurationTransfer);

            Assert.True(hostConfigurationAfterConverter.OkStatus);
            Assert.True(hostConfiguration.Equals(hostConfigurationAfterConverter.Value));
        }
    }
}