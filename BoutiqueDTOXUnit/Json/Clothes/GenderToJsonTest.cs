using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Пол одежды. Конвертация в Json
    /// </summary>
    public class GenderToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();

            string json = JsonSerializer.Serialize(genderTransfer);
            var genderAfterJson = JsonSerializer.Deserialize<GenderTransfer>(json);

            Assert.True(genderAfterJson?.Equals(genderTransfer));
        }
    }
}