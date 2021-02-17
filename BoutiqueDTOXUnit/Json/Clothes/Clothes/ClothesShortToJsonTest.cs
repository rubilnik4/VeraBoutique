using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes.Clothes
{
    /// <summary>
    /// Одежда. Конвертация в Json
    /// </summary>
    public class ClothesShortToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothesShortTransfer = ClothesTransfersData.ClothesShortTransfers.First();

            string json = JsonConvert.SerializeObject(clothesShortTransfer);
            var clothesShortAfterJson = JsonConvert.DeserializeObject<ClothesTransfer>(json);

            Assert.True(clothesShortAfterJson?.Equals(clothesShortTransfer));
        }
    }
}