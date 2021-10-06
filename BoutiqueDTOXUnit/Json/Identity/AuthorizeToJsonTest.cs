using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
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

            string json = JsonConvert.SerializeObject(authorizeTransfer);
            var authorizeAfterJson = JsonConvert.DeserializeObject<AuthorizeTransfer>(json);
            
            Assert.True(authorizeAfterJson?.Equals(authorizeTransfer));
        }
    }
}