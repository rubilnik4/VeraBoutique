using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
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
        public async Task ToTransferValueJson_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();
            string genderJson = JsonConvert.SerializeObject(genderTransfer);
            var taskJson = Task.FromResult(genderJson);

            var genderTransferAfter = await taskJson.ToTransferValueJsonAsync<GenderTransfer>();
            var genderJsonAfter = genderTransferAfter.Value.ToJsonTransfer();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.Equals(genderTransfer));
            Assert.True(genderJsonAfter.Value.Equals(genderJson));
        }

        /// <summary>
        /// Корректное преобразование
        /// </summary>
        [Fact]
        public async Task ToTransferCollectionJson_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers;
            string genderJson = JsonConvert.SerializeObject(genderTransfer);
            var taskJson = Task.FromResult(genderJson);

            var genderTransferAfter = await taskJson.ToTransferCollectionJsonAsync<GenderTransfer>();
            var genderJsonAfter = genderTransferAfter.Value.ToJsonTransfer();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.SequenceEqual(genderTransfer));
            Assert.True(genderJsonAfter.Value.Equals(genderJson));
        }
    }
}