using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Functional.Models.Enums;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.Json.Sync
{
    /// <summary>
    /// Методы расширения для json. Тесты
    /// </summary>
    public class JsonExtensionsTest
    {
        /// <summary>
        /// Корректное преобразование
        /// </summary>
        [Fact]
        public void ToTransferJson_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();
            string genderJson = JsonConvert.SerializeObject(genderTransfer);

            var genderTransferAfter = genderJson.ToTransferJson<GenderType, GenderTransfer>();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.Equals(genderTransfer));
        }

        /// <summary>
        /// Некорректное преобразование
        /// </summary>
        [Fact]
        public void ToTransferJson_Error()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();
            string genderJson = JsonConvert.SerializeObject(genderTransfer);

            var genderTransferAfter = genderJson.ToTransferJson<string, ColorTransfer>();

            Assert.True(genderTransferAfter.HasErrors);
            Assert.True(genderTransferAfter.Errors.First().ErrorResultType == ErrorResultType.JsonConvertion);
        }
    }
}