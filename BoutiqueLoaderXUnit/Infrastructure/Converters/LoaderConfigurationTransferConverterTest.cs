using System.Linq;
using BoutiqueLoader.Models.Implementations.Configuration;
using BoutiqueLoaderXUnit.Data.Transfers.Configuration;
using BoutiqueLoaderXUnit.Infrastructure.Mocks.Converters;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueLoaderXUnit.Infrastructure.Converters
{
    public class LoaderConfigurationTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var loaderConfiguration = LoaderConfigurationData.LoaderConfigurationDomains.First();
            var loaderConfigurationTransferConverter = LoaderConfigurationTransferConverterMock.LoaderConfigurationTransferConverter;

            var loaderConfigurationTransfer = loaderConfigurationTransferConverter.ToTransfer(loaderConfiguration);
            var loaderConfigurationAfterConverter = loaderConfigurationTransferConverter.FromTransfer(loaderConfigurationTransfer);

            Assert.True(loaderConfigurationAfterConverter.OkStatus);
            Assert.True(loaderConfiguration.Equals(loaderConfigurationAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели в трансферную модель. Ошибка параметров подключения
        /// </summary>
        [Fact]
        public void ToTransfer_HostError()
        {
            var hostConfigurationNull = new LoaderConfigurationTransfer(null!);
            var loaderConfigurationTransferConverter = LoaderConfigurationTransferConverterMock.LoaderConfigurationTransferConverter;

            var loaderConfigurationAfterConverter = loaderConfigurationTransferConverter.FromTransfer(hostConfigurationNull);

            Assert.True(loaderConfigurationAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(loaderConfigurationAfterConverter.Errors.First());
        }
    }
}