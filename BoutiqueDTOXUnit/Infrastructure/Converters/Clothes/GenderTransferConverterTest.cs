using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTOXUnit.Data;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель. Тесты
    /// </summary>
    public class GenderTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var gender = GenderData.GetGendersDomain().First();
            var genderEntityConverter = new GenderTransferConverter();

            var genderEntity = genderEntityConverter.ToTransfer(gender);
            var genderAfterConverter = genderEntityConverter.FromTransfer(genderEntity);

            Assert.True(gender.Equals(genderAfterConverter));
        }
    }
}