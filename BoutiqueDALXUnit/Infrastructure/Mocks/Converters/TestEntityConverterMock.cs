using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Тестовый конвертер сущностей
    /// </summary>
    public class TestEntityConverterMock
    {
        /// <summary>
        /// Тестовый конвертер сущностей
        /// </summary>
        public static ITestEntityConverter TestEntityConverter =>
            new TestEntityConverter(new TestIncludeEntityConverter());
    }
}