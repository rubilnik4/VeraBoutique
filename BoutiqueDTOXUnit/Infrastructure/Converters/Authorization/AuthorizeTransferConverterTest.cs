using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Authorization
{
    /// <summary>
    /// Конвертер логина и пароля в трансферную модель. Тесты
    /// </summary>
    public class AuthorizeTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели логина и пароля в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var authorize = AuthorizeData.AuthorizeDomains.First();
            var authorizeTransferConverter = new AuthorizeTransferConverter();

            var authorizesTransfer = authorizeTransferConverter.ToTransfer(authorize);
            var authorizeAfterConverter = authorizeTransferConverter.FromTransfer(authorizesTransfer);

            Assert.True(authorizeAfterConverter.OkStatus);
            Assert.True(authorize.Equals(authorizeAfterConverter.Value));
        }
    }
}