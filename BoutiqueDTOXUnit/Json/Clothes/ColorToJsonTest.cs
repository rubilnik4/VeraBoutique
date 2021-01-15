using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Цвет одежды. Конвертация в Json
    /// </summary>
    public class ColorToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var colorTransfer = ColorTransfersData.ColorTransfers.First();

            string json = JsonSerializer.Serialize(colorTransfer);
            var colorAfterJson = JsonSerializer.Deserialize<ColorTransfer>(json);

            Assert.True(colorAfterJson?.Equals(colorTransfer));
        }
    }
}