using Functional.FunctionalExtensions;
using Xunit;

namespace FunctionalXUnit.Models.FunctionalExtensions
{
    /// <summary>
    /// Методы расширения для преобразования типов. Тесты
    /// </summary>
    public class MapExtensionsTest
    {
        /// <summary>
        /// Проверка преобразование типов с помощью функции. Из числа в строку
        /// </summary>
        [Fact]
        public void Map_IntToString()
        {
            const int number = 2;

            string stringFromNumber = number.Map(numberConverting => numberConverting.ToString());

            Assert.Equal("2", stringFromNumber);
        }
    }
}