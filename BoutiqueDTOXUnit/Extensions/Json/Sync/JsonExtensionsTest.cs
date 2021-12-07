using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.Models.Enums;
using Newtonsoft.Json;
using ResultFunctional.Models.Implementations.Errors.ConversionErrors;
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

            var genderTransferAfter = genderJson.ToTransferValueJson<GenderTransfer>();
            var genderJsonAfter = genderTransferAfter.Value.ToJsonTransfer();

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

            var genderTransferAfter = genderJson.ToTransferCollectionJson<GenderTransfer>();
            var genderJsonAfter = genderTransferAfter.Value.ToJsonTransfer();

            Assert.True(genderTransferAfter.OkStatus);
            Assert.True(genderTransferAfter.Value.SequenceEqual(genderTransfer));
            Assert.True(genderJsonAfter.Value.Equals(genderJson));
        }

        /// <summary>
        /// Проверка схемы
        /// </summary>
        [Fact]
        public void JsonScheme()
        {
            var test = TestTransferData.TestTransfers.First();
            var json = test.ToJsonTransfer();
            const string jsonScheme =
            @"{
              'type': 'object',
              'properties': {
                  'TestEnum': {'type':'integer'},
                  'Name': {'type': 'string'},
              }
            }";

            var resultValid = json.Value.IsJsonSchemeValid(jsonScheme);

            Assert.True(resultValid);
        }

        /// <summary>
        /// Проверка схемы
        /// </summary>
        [Fact]
        public void JsonScheme_Error()
        {
            var test = TestTransferData.TestTransfers.First();
            var json = test.ToJsonTransfer();
            const string jsonScheme =
            @"{
              'type': 'object',
              'properties': {
                  'TestEnum': {'type':'string'},
                  'Name': {'type': 'string'},
              }
            }";

            var resultValid = json.Value.IsJsonSchemeValid(jsonScheme);

            Assert.False(resultValid);
        }
    }
}