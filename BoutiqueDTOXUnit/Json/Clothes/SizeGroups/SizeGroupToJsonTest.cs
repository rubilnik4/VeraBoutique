using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
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

            string json = JsonConvert.SerializeObject(sizeGroupTransfer);
            var sizeGroupAfterJson = JsonConvert.DeserializeObject<SizeGroupMainTransfer>(json);

            Assert.True(sizeGroupAfterJson?.Equals(sizeGroupTransfer));
        }
    }
}