using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
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

            string json = JsonSerializer.Serialize(clothesShortTransfer);
            var clothesShortAfterJson = JsonSerializer.Deserialize<ClothesShortTransfer>(json);

            Assert.True(clothesShortAfterJson?.Equals(clothesShortTransfer));
        }
    }
}