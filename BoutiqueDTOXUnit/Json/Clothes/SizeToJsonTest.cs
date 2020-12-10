using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Размеры одежды. Конвертация в Json
    /// </summary>
    public class SizeToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var sizeTransfer = SizeTransfersData.SizeTransfers.First();

            string json = JsonSerializer.Serialize(sizeTransfer);
            var sizeAfterJson = JsonSerializer.Deserialize<SizeTransfer>(json);
            
            Assert.True(sizeAfterJson?.Equals(sizeTransfer));
        }
    }
}