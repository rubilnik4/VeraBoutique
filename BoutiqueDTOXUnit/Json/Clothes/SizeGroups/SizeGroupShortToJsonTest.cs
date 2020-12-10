using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Конвертация в Json
    /// </summary>
    public class SizeGroupShortToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var sizeGroupShortTransfer = SizeGroupTransfersData.SizeGroupTransfers.First();

            string json = JsonSerializer.Serialize(sizeGroupShortTransfer);
            var sizeGroupShortAfterJson = JsonSerializer.Deserialize<SizeGroupShortTransfer>(json);

            Assert.True(sizeGroupShortAfterJson?.Equals(sizeGroupShortTransfer));
        }
    }
}