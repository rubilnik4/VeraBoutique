using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Data.Transfers.Configuration;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Configuration
{
    /// <summary>
    /// Параметры подключения. Конвертация в Json
    /// </summary>
    public class HostConfigurationToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var hostConfigurationTransfer = HostConfigurationTransferData.HostConfigurationTransfers.First();

            string json = JsonSerializer.Serialize(hostConfigurationTransfer);
            var hostConfigurationAfterJson = JsonSerializer.Deserialize<HostConfigurationTransfer>(json);
            
            Assert.True(hostConfigurationAfterJson?.Equals(hostConfigurationTransfer));
        }
    }
}