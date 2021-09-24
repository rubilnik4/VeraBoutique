using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity
{
    /// <summary>
    /// Конвертер логина и пароля
    /// </summary>
    public static class AuthorizeTransferConverterMock
    {
        /// <summary>
        /// Конвертер логина и пароля в трансферную модель
        /// </summary>
        public static IAuthorizeTransferConverter AuthorizeTransferConverter =>
            new AuthorizeTransferConverter();
    }
}