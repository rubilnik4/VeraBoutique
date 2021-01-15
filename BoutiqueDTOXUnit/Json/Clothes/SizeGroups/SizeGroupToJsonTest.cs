using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Конвертация в Json
    /// </summary>
    public class SizeGroupToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var sizeGroupTransfer = SizeGroupTransfersData.SizeGroupTransfers.First();

            string json = JsonSerializer.Serialize(sizeGroupTransfer);
            var sizeGroupAfterJson = JsonSerializer.Deserialize<SizeGroupTransfer>(json);

            Assert.True(sizeGroupAfterJson?.Equals(sizeGroupTransfer));
        }
    }
}