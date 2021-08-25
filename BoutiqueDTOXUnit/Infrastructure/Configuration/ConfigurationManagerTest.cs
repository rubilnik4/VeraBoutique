using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Infrastructure.Implementations.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Configuration;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Configuration;
using Functional.FunctionalExtensions.Sync;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationManagerTest: ConfigurationManager<TestEnum, ITestDomain, TestTransfer>
    {
        public ConfigurationManagerTest()
            : base(TestTransferConverterMock.TestTransferConverter)
        { }

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public override string GetConfigurationText() =>
            ConfigurationTextOk;

        /// <summary>
        /// Получить конфигурацию в текстовом виде асинхронно
        /// </summary>
        public override async Task<string> GetConfigurationTextAsync() =>
            await Task.FromResult(ConfigurationTextOk);

        /// <summary>
        /// Получить корректную конфигурацию
        /// </summary>
        [Fact]
        public void GetConfiguration_Ok()
        {
            var configuration = GetConfiguration();

            Assert.True(configuration.OkStatus);
            Assert.True(configuration.Value.Equals(TestData.TestDomains.First()));
        }

        /// <summary>
        /// Получить корректную конфигурацию
        /// </summary>
        [Fact]
        public async Task GetConfigurationAsync_Ok()
        {
            var configuration = await GetConfigurationAsync();

            Assert.True(configuration.OkStatus);
            Assert.True(configuration.Value.Equals(TestData.TestDomains.First()));
        }

        /// <summary>
        /// Пример текста
        /// </summary>
        private static string ConfigurationTextOk => 
            TestTransferData.TestTransfers.First().
            Map(JsonConvert.SerializeObject);
    }
}