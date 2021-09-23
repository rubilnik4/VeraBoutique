using System.Linq;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data.Transfers.Authorize;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Identity
{
    /// <summary>
    /// Личная информация. Конвертация в Json
    /// </summary>
    public class PersonalToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var personalTransfer = PersonalTransferData.PersonalTransfers.First();

            string json = JsonConvert.SerializeObject(personalTransfer);
            var personalAfterJson = JsonConvert.DeserializeObject<PersonalTransfer>(json);

            Assert.True(personalAfterJson?.Equals(personalTransfer));
        }
    }
}