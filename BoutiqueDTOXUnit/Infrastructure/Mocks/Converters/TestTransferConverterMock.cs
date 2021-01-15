using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Тестовый конвертер трансферных моделей
    /// </summary>
    public class TestTransferConverterMock
    {
        /// <summary>
        /// Тестовый конвертер трансферных моделей
        /// </summary>
        public static ITestTransferConverter TestTransferConverter =>
            new TestTransferConverter(new TestIncludesTransferConverter());
    }
}