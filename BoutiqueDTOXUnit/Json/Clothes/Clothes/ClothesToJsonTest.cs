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
    public class ClothesToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothesTransfer = ClothesTransfersData.ClothesTransfers.First();

            string json = JsonConvert.SerializeObject(clothesTransfer);
            var clothesAfterJson = JsonConvert.DeserializeObject<ClothesFullTransfer>(json);

            Assert.True(clothesAfterJson?.Equals(clothesTransfer));
        }
    }
}