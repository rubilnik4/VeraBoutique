using BoutiqueDTO.Infrastructure.Implementations.Converters.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity
{
    /// <summary>
    /// Конвертер личных данных в трансферную модель
    /// </summary>
    public static class PersonalTransferConverterMock
    {
        /// <summary>
        /// Конвертер личных данных в трансферную модель
        /// </summary>
        public static IPersonalTransferConverter PersonalTransferConverter =>
            new PersonalTransferConverter();
    }
}