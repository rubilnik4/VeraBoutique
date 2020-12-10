using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes.ClothesTypes
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

            string json = JsonSerializer.Serialize(clothesTypeTransfer);
            var clothesTypeAfterJson = JsonSerializer.Deserialize<ClothesTypeTransfer>(json);

            Assert.True(clothesTypeAfterJson?.Equals(clothesTypeTransfer));
        }
    }
}