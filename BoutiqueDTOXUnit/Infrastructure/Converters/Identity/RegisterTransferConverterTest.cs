using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Identity
{
    /// <summary>
    /// Конвертер регистрации в трансферную модель. Тесты
    /// </summary>
    public class RegisterTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели регистрации в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var register = RegisterData.RegisterDomains.First();
            var registerTransferConverter = RegisterTransferConverterMock.RegisterTransferConverter;

            var registerTransfer = registerTransferConverter.ToTransfer(register);
            var registerAfterConverter = registerTransferConverter.FromTransfer(registerTransfer);

            Assert.True(registerAfterConverter.OkStatus);
            Assert.True(register.Equals(registerAfterConverter.Value));
        }
    }
}