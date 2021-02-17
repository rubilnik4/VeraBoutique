using System.Linq;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
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

            string json = JsonConvert.SerializeObject(clothesTypeShortTransfer);
            var clothesTypeShortAfterJson = JsonConvert.DeserializeObject<ClothesTypeShortTransfer>(json);

            Assert.True(clothesTypeShortAfterJson?.Equals(clothesTypeShortTransfer));
        }
    }
}