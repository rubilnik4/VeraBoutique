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
        public void ToJsonMain_Ok()
        {
            var clothesTransfer = ClothesTransfersData.ClothesMainTransfers.First();

            string json = JsonConvert.SerializeObject(clothesTransfer);
            var clothesAfterJson = JsonConvert.DeserializeObject<ClothesMainTransfer>(json);

            Assert.True(clothesAfterJson?.Equals(clothesTransfer));
        }

        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothesTransfer = ClothesTransfersData.ClothesTransfers.First();

            string json = JsonConvert.SerializeObject(clothesTransfer);
            var clothesShortAfterJson = JsonConvert.DeserializeObject<ClothesTransfer>(json);

            Assert.True(clothesShortAfterJson?.Equals(clothesTransfer));
        }
    }
}