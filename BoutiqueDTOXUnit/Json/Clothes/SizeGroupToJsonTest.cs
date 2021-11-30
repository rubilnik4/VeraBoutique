using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
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
        public void ToJsonMain_Ok()
        {
            var sizeGroupMainTransfer = SizeGroupTransfersData.SizeGroupMainTransfers.First();

            string json = JsonConvert.SerializeObject(sizeGroupMainTransfer);
            var sizeGroupAfterJson = JsonConvert.DeserializeObject<SizeGroupMainTransfer>(json);

            Assert.True(sizeGroupAfterJson?.Equals(sizeGroupMainTransfer));
        }

        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var sizeGroupShortTransfer = SizeGroupTransfersData.SizeGroupTransfers.First();

            string json = JsonConvert.SerializeObject(sizeGroupShortTransfer);
            var sizeGroupShortAfterJson = JsonConvert.DeserializeObject<SizeGroupTransfer>(json);

            Assert.True(sizeGroupShortAfterJson?.Equals(sizeGroupShortTransfer));
        }
    }
}