using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
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

            string json = JsonConvert.SerializeObject(genderTransfer);
            var genderAfterJson = JsonConvert.DeserializeObject<GenderTransfer>(json);

            Assert.True(genderAfterJson?.Equals(genderTransfer));
        }
    }
}