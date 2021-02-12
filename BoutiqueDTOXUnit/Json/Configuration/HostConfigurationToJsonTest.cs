using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Data.Transfers.Configuration;
using Newtonsoft.Json;
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
            var hostConfigurationTransfer = HostConfigurationTransfersData.HostConfigurationTransfers.First();

            string json = JsonConvert.SerializeObject(hostConfigurationTransfer);
            var hostConfigurationAfterJson = JsonConvert.DeserializeObject<HostConfigurationTransfer>(json);
            
            Assert.True(hostConfigurationAfterJson?.Equals(hostConfigurationTransfer));
        }
    }
}