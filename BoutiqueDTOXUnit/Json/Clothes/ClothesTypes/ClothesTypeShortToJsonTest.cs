using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes.ClothesTypes
{
    /// <summary>
    /// Тип одежды. Конвертация в Json
    /// </summary>
    public class ClothesTypeShortToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothesTypeShortTransfer = ClothesTypeTransfersData.ClothesTypeShortTransfers.First();

            string json = JsonSerializer.Serialize(clothesTypeShortTransfer);
            var clothesTypeShortAfterJson = JsonSerializer.Deserialize<ClothesTypeShortTransfer>(json);

            Assert.True(clothesTypeShortAfterJson?.Equals(clothesTypeShortTransfer));
        }
    }
}