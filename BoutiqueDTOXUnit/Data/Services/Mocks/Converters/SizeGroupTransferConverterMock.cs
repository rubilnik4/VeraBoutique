using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;

namespace BoutiqueDTOXUnit.Data.Services.Mocks.Converters
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель
    /// </summary>
    public class SizeGroupTransferConverterMock
    {
        /// <summary>
        /// Конвертер группы размеров одежды в трансферную модель
        /// </summary>
        public static ISizeGroupTransferConverter SizeGroupTransferConverter =>
            new SizeGroupTransferConverter(SizeTransferConverterMock.SizeTransferConverter);
    }
}