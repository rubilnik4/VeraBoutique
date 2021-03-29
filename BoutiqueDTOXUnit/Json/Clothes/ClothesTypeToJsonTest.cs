using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Тип одежды. Конвертация в Json
    /// </summary>
    public class ClothesTypeToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothesTypeTransfer = ClothesTypeTransfersData.ClothesTypeTransfers.First();

            string json = JsonConvert.SerializeObject(clothesTypeTransfer);
            var clothesTypeShortAfterJson = JsonConvert.DeserializeObject<ClothesTypeTransfer>(json);

            Assert.True(clothesTypeShortAfterJson?.Equals(clothesTypeTransfer));
        }
    }
}