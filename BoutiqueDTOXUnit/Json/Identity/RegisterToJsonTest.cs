using System.Linq;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Identity
{
    /// <summary>
    /// Регистрация. Конвертация в Json
    /// </summary>
    public class RegisterToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var registerTransfer = RegisterTransferData.RegisterTransfers.First();

            string json = JsonConvert.SerializeObject(registerTransfer);
            var registerAfterJson = JsonConvert.DeserializeObject<RegisterTransfer>(json);

            Assert.True(registerAfterJson?.Equals(registerTransfer));
        }
    }
}