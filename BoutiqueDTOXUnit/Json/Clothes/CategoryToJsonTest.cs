using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Категория одежды. Конвертация в Json
    /// </summary>
    public class CategoryToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var categoryTransfer = CategoryTransfersData.CategoryTransfers.First();

            string json = JsonConvert.SerializeObject(categoryTransfer);
            var categoryAfterJson = JsonConvert.DeserializeObject<CategoryTransfer>(json);

            Assert.True(categoryAfterJson?.Equals(categoryAfterJson));
        }
    }
}