using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Преобразования модели группы размера одежды в модель базы данных
    /// </summary>
    public class SizeGroupEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        public static ISizeGroupEntityConverter SizeGroupEntityConverter =>
            new SizeGroupEntityConverter(SizeEntityConverterMock.SizeEntityConverter);
    }
}