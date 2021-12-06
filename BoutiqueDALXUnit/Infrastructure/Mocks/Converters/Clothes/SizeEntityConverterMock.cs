using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели размера одежды в модель базы данных
    /// </summary>
    public class SizeEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели размера одежды в модель базы данных
        /// </summary>
        public static ISizeEntityConverter SizeEntityConverter =>
            new SizeEntityConverter();
    }
}