using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
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

            string json = JsonConvert.SerializeObject(clothesTypeTransfer);
            var clothesTypeAfterJson = JsonConvert.DeserializeObject<ClothesTypeTransfer>(json);

            Assert.True(clothesTypeAfterJson?.Equals(clothesTypeTransfer));
        }
    }
}