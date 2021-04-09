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
using Functional.Models.Interfaces.Result;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        public override async Task<string> GetConfigurationText() =>
            await Task.FromResult(ConfigurationTextOk);

        /// <summary>
        /// Получить корректную конфигурацию
        /// </summary>
        [Fact]
        public async Task GetConfiguration_Ok()
        {
            var configuration = await GetConfiguration();

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