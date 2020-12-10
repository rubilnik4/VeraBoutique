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
    public class ClothesToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothesTransfer = ClothesTransfersData.ClothesTransfers.First();

            string json = JsonSerializer.Serialize(clothesTransfer);
            var clothesAfterJson = JsonSerializer.Deserialize<ClothesTransfer>(json);

            Assert.True(clothesAfterJson?.Equals(clothesTransfer));
        }
    }
}