using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Functional.Models.Enums;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.Json.Async
{
    /// <summary>
    /// Асинхронные методы расширения для json. Тесты
    /// </summary>
    public class JsonExtensionsAsyncTest
    {
        /// <summary>
        /// Корректное преобразование
        /// </summary>
        [Fact]
        public async Task ToTransferJsonAsync_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();
            var genderJson = Task.FromResult(JsonConvert.SerializeObject(genderTransfer));

            var genderTransferAfter = await genderJson.ToTransferJsonAsync<GenderType, GenderTransfer>();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.Equals(genderTransfer));
        }

        /// <summary>
        /// Некорректное преобразование
        /// </summary>
        [Fact]
        public async Task ToTransferJsonAsync_Error()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();
            var genderJson = Task.FromResult(JsonConvert.SerializeObject(genderTransfer));

            var genderTransferAfter = await genderJson.ToTransferJsonAsync<string, ColorTransfer>();

            Assert.True(genderTransferAfter.HasErrors);
            Assert.True(genderTransferAfter.Errors.First().ErrorResultType == ErrorResultType.JsonConvertion);
        }
    }
}