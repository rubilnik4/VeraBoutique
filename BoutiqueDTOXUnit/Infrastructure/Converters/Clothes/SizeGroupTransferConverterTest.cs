﻿using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель. Тесты
    /// </summary>
    public class SizeGroupTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели размеров одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var sizeGroup = SizeGroupData.SizeGroupDomain.First();
            var sizeTransferConverter = new SizeTransferConverter();
            var sizeGroupTransferConverter = new SizeGroupTransferConverter(sizeTransferConverter);

            var sizeGroupTransfer = sizeGroupTransferConverter.ToTransfer(sizeGroup);
            var sizeGroupAfterConverter = sizeGroupTransferConverter.FromTransfer(sizeGroupTransfer);

            Assert.True(sizeGroup.Equals(sizeGroupAfterConverter));
        }
    }
}