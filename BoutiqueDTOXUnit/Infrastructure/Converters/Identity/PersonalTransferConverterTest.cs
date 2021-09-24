using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Identity
{
    /// <summary>
    /// Конвертер личных данных в трансферную модель. Тесты
    /// </summary>
    public class PersonalTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели личных данных в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var personal = PersonalData.PersonalDomains.First();
            var personalTransferConverter = PersonalTransferConverterMock.PersonalTransferConverter;

            var personalTransfer = personalTransferConverter.ToTransfer(personal);
            var personalAfterConverter = personalTransferConverter.FromTransfer(personalTransfer);

            Assert.True(personalAfterConverter.OkStatus);
            Assert.True(personal.Equals(personalAfterConverter.Value));
        }
    }
}