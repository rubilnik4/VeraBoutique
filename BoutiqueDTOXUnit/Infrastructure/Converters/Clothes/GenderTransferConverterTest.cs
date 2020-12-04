using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Services.Mocks.Converters;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель. Тесты
    /// </summary>
    public class GenderTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели типа пола в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var gender = GenderData.GendersDomain.First();
            var genderEntityConverter = GenderTransferConverterMock.GenderTransferConverter;

            var genderTransfer = genderEntityConverter.ToTransfer(gender);
            var genderAfterConverter = genderEntityConverter.FromTransfer(genderTransfer);

            Assert.True(genderAfterConverter.OkStatus);
            Assert.True(gender.Equals(genderAfterConverter.Value));
        }
    }
}