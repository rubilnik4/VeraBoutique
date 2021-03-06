﻿using System.Linq;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using Newtonsoft.Json;
using Xunit;

namespace BoutiqueDTOXUnit.Json.Clothes
{
    /// <summary>
    /// Размеры одежды. Конвертация в Json
    /// </summary>
    public class HostConfigurationToJsonTest
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var sizeTransfer = SizeTransfersData.SizeTransfers.First();

            string json = JsonConvert.SerializeObject(sizeTransfer);
            var sizeAfterJson = JsonConvert.DeserializeObject<SizeTransfer>(json);
            
            Assert.True(sizeAfterJson?.Equals(sizeTransfer));
        }
    }
}