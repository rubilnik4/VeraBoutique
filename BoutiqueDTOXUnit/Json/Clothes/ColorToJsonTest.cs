﻿using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Цвет одежды. Конвертация в Json
    /// </summary>
    public class ColorToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var colorTransfer = ColorTransfersData.ColorTransfers.First();

            string json = JsonConvert.SerializeObject(colorTransfer);
            var colorAfterJson = JsonConvert.DeserializeObject<ColorTransfer>(json);

            Assert.True(colorAfterJson?.Equals(colorTransfer));
        }
    }
}