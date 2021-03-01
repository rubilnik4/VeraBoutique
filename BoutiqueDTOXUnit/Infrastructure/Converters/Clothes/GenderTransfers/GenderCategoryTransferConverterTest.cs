using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.GenderTransfers
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель. Тесты
    /// </summary>
    public class GenderCategoryTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели типа пола в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var gender = GenderData.GenderCategoryDomains.First();
            var genderCategoryTransferConverter = GenderTransferConverterMock.GenderCategoryTransferConverter;

            var genderTransfer = genderCategoryTransferConverter.ToTransfer(gender);
            var genderAfterConverter = genderCategoryTransferConverter.FromTransfer(genderTransfer);

            Assert.True(genderAfterConverter.OkStatus);
            Assert.True(gender.Equals(genderAfterConverter.Value));
        }
    }
}