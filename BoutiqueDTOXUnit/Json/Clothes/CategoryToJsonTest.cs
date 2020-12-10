using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
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

            string json = JsonSerializer.Serialize(categoryTransfer);
            var categoryAfterJson = JsonSerializer.Deserialize<CategoryTransfer>(json);

            Assert.True(categoryAfterJson?.Equals(categoryAfterJson));
        }
    }
}