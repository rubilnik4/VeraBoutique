using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDTOXUnit.Data.Services.Mocks.Converters
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public class CategoryTransferConverterMock
    {
        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        public static ICategoryTransferConverter CategoryTransferConverter =>
            new CategoryTransferConverter();
    }
}