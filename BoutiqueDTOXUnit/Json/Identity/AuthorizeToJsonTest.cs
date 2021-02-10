using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Identity
{
    /// <summary>
    /// Параметры авторизации. Конвертация в Json
    /// </summary>
    public class AuthorizeToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var authorizeTransfer = AuthorizeTransfersData.AuthorizeTransfers.First();

            string json = JsonSerializer.Serialize(authorizeTransfer);
            var authorizeAfterJson = JsonSerializer.Deserialize<AuthorizeTransfer>(json);
            
            Assert.True(authorizeAfterJson?.Equals(authorizeTransfer));
        }
    }
}