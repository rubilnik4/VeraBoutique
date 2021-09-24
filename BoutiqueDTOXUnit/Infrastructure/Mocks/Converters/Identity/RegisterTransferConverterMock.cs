using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity
{
    /// <summary>
    /// Конвертер регистрации в трансферную модель
    /// </summary>
    public static class RegisterTransferConverterMock
    {
        /// <summary>
        /// Конвертер регистрации в трансферную модель
        /// </summary>
        public static IRegisterTransferConverter RegisterTransferConverter =>
            new RegisterTransferConverter(AuthorizeTransferConverterMock.AuthorizeTransferConverter,
                                          PersonalTransferConverterMock.PersonalTransferConverter);
    }
}