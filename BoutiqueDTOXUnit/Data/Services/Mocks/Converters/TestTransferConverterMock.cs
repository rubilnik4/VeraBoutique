using BoutiqueDTOXUnit.Data.Services.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;

namespace BoutiqueDTOXUnit.Data.Services.Mocks.Converters
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