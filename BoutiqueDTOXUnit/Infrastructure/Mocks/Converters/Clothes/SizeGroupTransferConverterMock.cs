using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;

namespace BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes
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

        /// <summary>
        /// Конвертер базовых данных группы размеров одежды в трансферную модель
        /// </summary>
        public static ISizeGroupShortTransferConverter SizeGroupTransferConverter =>
            new SizeGroupShortTransferConverter();
    }
}