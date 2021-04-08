using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
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
        public void ToTransferValueJson_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers.First();
            string genderJson = JsonConvert.SerializeObject(genderTransfer);

            var genderTransferAfter = genderJson.ToTransferValueJson<GenderType, GenderTransfer>();
            var genderJsonAfter = genderTransferAfter.Value.ToJsonTransfer<GenderType, GenderTransfer>();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.Equals(genderTransfer));
            Assert.True(genderJsonAfter.Value.Equals(genderJson));
        }

        /// <summary>
        /// Корректное преобразование
        /// </summary>
        [Fact]
        public void ToTransferCollectionJson_Ok()
        {
            var genderTransfer = GenderTransfersData.GenderTransfers;
            string genderJson = JsonConvert.SerializeObject(genderTransfer);

            var genderTransferAfter = genderJson.ToTransferCollectionJson<GenderType, GenderTransfer>();
            var genderJsonAfter = genderTransferAfter.Value.ToJsonTransfer<GenderType, GenderTransfer>();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.Equals(genderTransfer));
            Assert.True(genderJsonAfter.Value.Equals(genderJson));
        }
    }
}