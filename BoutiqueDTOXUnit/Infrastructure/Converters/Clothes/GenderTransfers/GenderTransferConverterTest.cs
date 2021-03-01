using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.GenderTransfers
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
            var gender = GenderData.GenderDomains.First();
            var genderEntityConverter = GenderTransferConverterMock.GenderTransferConverter;

            var genderTransfer = genderEntityConverter.ToTransfer(gender);
            var genderAfterConverter = genderEntityConverter.FromTransfer(genderTransfer);

            Assert.True(genderAfterConverter.OkStatus);
            Assert.True(gender.Equals(genderAfterConverter.Value));
        }
    }
}