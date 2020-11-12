using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Преобразования модели типа пола и модель базы данных
    /// </summary>
    public class GenderEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        public static IGenderEntityConverter GenderEntityConverter =>
             new GenderEntityConverter();
    }
}