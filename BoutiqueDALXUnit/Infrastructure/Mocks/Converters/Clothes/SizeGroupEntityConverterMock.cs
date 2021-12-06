using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes
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
            new SizeGroupEntityConverter();

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        public static ISizeGroupMainEntityConverter SizeGroupMainEntityConverter =>
            new SizeGroupMainEntityConverter(SizeEntityConverterMock.SizeEntityConverter);
    }
}